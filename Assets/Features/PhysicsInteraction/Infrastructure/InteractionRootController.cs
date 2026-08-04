using Feature.CameraFeature;
using Feature.PlayerFeature;
using System;
using UnityEngine;
using Zenject;

namespace Feature.PhysicsInteraction
{
    public class InteractionRootController : IInitializable, IDisposable, IFixedTickable
    {
        private InteractionRoot _interactionRoot;
        private IReadOnlyCamera _readOnlyCamera;
        private IReadOnlyCharacterMotor _characterMotor;
        private Player _player;

        public InteractionRootController(InteractionRoot interactionRoot, IReadOnlyCamera readOnlyCamera, IReadOnlyCharacterMotor characterMotor, Player player)
        {
            _interactionRoot = interactionRoot;
            _readOnlyCamera = readOnlyCamera;
            _characterMotor = characterMotor;
            _player = player;
        }
        public void Initialize()
        {
            TeleportToPlayer();

            _player.Respawned += TeleportToPlayer;
        }

        public void Dispose()
        {
            _player.Respawned -= TeleportToPlayer;
        }

        private void TeleportToPlayer()
        {
            _interactionRoot.SetPosition(_characterMotor.GetPosition());
            _interactionRoot.SetRotation(_characterMotor.GetRotation());
        }

        public void FixedTick()
        {
            UpdateRotation(Time.deltaTime);
            UpdatePosition(Time.deltaTime);
        }

        private void UpdateRotation(float dt)
        {
            Quaternion current = _interactionRoot.transform.rotation;
            Quaternion cameraRotation = _readOnlyCamera.Rotation;

            float smoothing = 100f;
            Quaternion desired = Quaternion.Slerp(current, cameraRotation, 1f - Mathf.Exp(-smoothing * dt));
            Quaternion delta = desired * Quaternion.Inverse(current);
            Vector3 angularVelocity = new Vector3(delta.x, delta.y, delta.z) * 2f / dt;
            _interactionRoot.SetAngularVelocity(angularVelocity);
        }

        private void UpdatePosition(float dt)
        {
            Vector3 current = _interactionRoot.GetPosition();
            Vector3 deltaPos = _characterMotor.GetPosition() - current;
            float distance = deltaPos.magnitude;

            float radius = 0.25f;
            float maxSpeed = 25f;

            float speed = maxSpeed * Mathf.Min(distance / radius, 1f);

            float maxSpeedThisFrame = distance / dt;
            speed = Mathf.Min(maxSpeed, maxSpeedThisFrame);

            Vector3 velocity = distance > 0.0001f
                ? deltaPos.normalized * speed
                : Vector3.zero;

            _interactionRoot.SetVelocity(velocity);
        }


    }
}
