using UnityEngine;

namespace Feature.PlayerFeature
{
    public class SlidePhysicsCalculator
    {
        public Vector3 UnstableSlideContinue(float slideFriction, float slideGravity, float slideSteerAccelerarion,
            Vector3 groundMovement, Vector3 groundAdj, Vector3 rawVelocity, Vector3 normal, float dt)
        {
            Vector3 hVelocity = new Vector3(rawVelocity.x, 0f, rawVelocity.z);

            hVelocity -= hVelocity * (slideFriction * dt);

            Vector3 gravOnSurface = Vector3.ProjectOnPlane(Vector3.down * slideGravity * dt, normal);
            hVelocity += new Vector3(gravOnSurface.x, 0f, gravOnSurface.z);

            float currentSpeed = hVelocity.magnitude;

            if (groundMovement.magnitude < 0.001f)
                groundMovement = hVelocity.normalized;

            Vector3 targetVelocity = groundMovement * currentSpeed;
            Vector3 steerForce = (targetVelocity - hVelocity) * slideSteerAccelerarion * dt;
            hVelocity += steerForce;
            hVelocity = Vector3.ClampMagnitude(hVelocity, currentSpeed);

            var velocity = new Vector3(hVelocity.x, groundAdj.y, hVelocity.z);

            return velocity;
        }

        public Vector3 UnstableSlideImpulse(Vector3 rawVelocity, Vector3 normal, float effectiveSlideStartSpeed)
        {
            Vector3 incomingVelocity = rawVelocity /*- groundAdj*/;
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(incomingVelocity, normal);

            Vector3 horizontalVelocity = new Vector3(projectedVelocity.x, 0f, projectedVelocity.z);
            float horizontalSpeed = horizontalVelocity.magnitude;

            Vector3 horizontalDir;
            if (horizontalSpeed > 0.0001f)
            {
                horizontalDir = horizontalVelocity.normalized;
            }
            else
            {
                Vector3 downSlope = Vector3.ProjectOnPlane(Vector3.down, normal);
                horizontalDir = new Vector3(downSlope.x, 0f, downSlope.z).normalized;
                horizontalSpeed = 0f;
            }

            float slideSpeed = Mathf.Max(effectiveSlideStartSpeed, horizontalSpeed);
            Vector3 initialVelocity = horizontalDir * slideSpeed;
            float slopeY = projectedVelocity.y;
            var velocity = new Vector3(initialVelocity.x, slopeY, initialVelocity.z);

            return velocity;
        }
    }
}