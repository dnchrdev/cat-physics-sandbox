using UnityEngine;

namespace Features.Player.Entities
{
    public static class LocomotionPhysics
    {
        private const float StopSpeedSqr = 0.01f;
        private const float NoInputSqr = 0.01f;
        private const float MinSlideSteer = 0.001f;
        private const float MinSlideSpeed = 0.0001f;

        public static (Vector3 velocity, Vector3 acceleration) Walk(
            Vector3 rawVelocity,
            Vector3 groundAdjustment,
            Vector3 groundNormal,
            bool wasOnStableGround,
            Vector3 requestedMovement,
            SurfaceProperties surface,
            float deltaTime)
        {
            var speed = surface.WalkSpeed;

            var currentPlanarVelocity = wasOnStableGround
                ? new Vector3(rawVelocity.x, 0f, rawVelocity.z)
                : Vector3.ProjectOnPlane(rawVelocity, groundNormal);

            if (currentPlanarVelocity.sqrMagnitude < StopSpeedSqr && requestedMovement.sqrMagnitude < NoInputSqr)
                currentPlanarVelocity = Vector3.zero;

            var desiredVelocity = requestedMovement * speed;
            var accelerationDirection = desiredVelocity - currentPlanarVelocity;

            var responseValue = requestedMovement.sqrMagnitude <= NoInputSqr
                ? -1f
                : Mathf.Clamp(Vector3.Dot(accelerationDirection.normalized, currentPlanarVelocity), -1f, 1f);

            var acceleration = Mathf.Lerp(
                surface.WalkMaxAccelerationResponse,
                surface.WalkMinAccelerationResponse,
                (responseValue + 1f) * 0.5f);

            var movementForce = accelerationDirection.normalized * acceleration * deltaTime;
            var finalHorizontal = currentPlanarVelocity + movementForce;

            var speedCap = Mathf.Max(currentPlanarVelocity.magnitude, speed);
            if (finalHorizontal.magnitude > speedCap)
                finalHorizontal = finalHorizontal.normalized * speedCap;

            var velocity = new Vector3(finalHorizontal.x, groundAdjustment.y, finalHorizontal.z);
            return (velocity, desiredVelocity - currentPlanarVelocity);
        }

        public static Vector3 Air(
            Vector3 rawVelocity,
            Vector3 requestedMovement,
            SurfaceProperties surface,
            float gravity,
            float deltaTime)
        {
            var planarVelocity = Vector3.ProjectOnPlane(rawVelocity, Vector3.up);
            var verticalVelocity = Vector3.Dot(rawVelocity, Vector3.up);

            if (requestedMovement.sqrMagnitude > 0f)
                planarVelocity += AirMovementForce(planarVelocity, requestedMovement, surface, deltaTime);

            verticalVelocity -= gravity * deltaTime;

            return new Vector3(planarVelocity.x, verticalVelocity, planarVelocity.z);
        }
        
        public static Vector3 JumpStart(Vector3 rawVelocity) =>
            rawVelocity.y > 0f ? new Vector3(rawVelocity.x, 0f, rawVelocity.z) : rawVelocity;

        public static Vector3 Jump(Vector3 startVelocity, Vector3 groundAdjustment, bool isStable, float jumpSpeed)
        {
            var cleanVelocity = startVelocity - groundAdjustment;
            var cleanVerticalSpeed = Vector3.Dot(cleanVelocity, Vector3.up);
            var jumpVerticalSpeed = Mathf.Max(cleanVerticalSpeed, jumpSpeed);
            var horizontalVelocity = Vector3.ProjectOnPlane(cleanVelocity, Vector3.up);

            var needsAdjustment = !isStable || groundAdjustment.y > 0f;

            return new Vector3(horizontalVelocity.x, jumpVerticalSpeed, horizontalVelocity.z)
                   + (needsAdjustment ? groundAdjustment : Vector3.zero);
        }

        public static Vector3 SlideStart(Vector3 rawVelocity, Vector3 normal, float minSlideSpeed)
        {
            var projectedVelocity = Vector3.ProjectOnPlane(rawVelocity, normal);
            var horizontalVelocity = new Vector3(projectedVelocity.x, 0f, projectedVelocity.z);
            var horizontalSpeed = horizontalVelocity.magnitude;

            Vector3 horizontalDirection;
            if (horizontalSpeed > MinSlideSpeed)
            {
                horizontalDirection = horizontalVelocity.normalized;
            }
            else
            {
                var downSlope = Vector3.ProjectOnPlane(Vector3.down, normal);
                horizontalDirection = new Vector3(downSlope.x, 0f, downSlope.z).normalized;
                horizontalSpeed = 0f;
            }

            var slideSpeed = Mathf.Max(minSlideSpeed, horizontalSpeed);
            var initialVelocity = horizontalDirection * slideSpeed;

            return new Vector3(initialVelocity.x, projectedVelocity.y, initialVelocity.z);
        }

        public static Vector3 SlideContinue(
            float slideFriction,
            float slideGravity,
            float steerAcceleration,
            Vector3 groundMovement,
            Vector3 groundAdjustment,
            Vector3 rawVelocity,
            Vector3 normal,
            float deltaTime)
        {
            var horizontalVelocity = new Vector3(rawVelocity.x, 0f, rawVelocity.z);
            horizontalVelocity -= horizontalVelocity * (slideFriction * deltaTime);

            var gravityOnSurface = Vector3.ProjectOnPlane(Vector3.down * slideGravity * deltaTime, normal);
            horizontalVelocity += new Vector3(gravityOnSurface.x, 0f, gravityOnSurface.z);

            var currentSpeed = horizontalVelocity.magnitude;

            if (groundMovement.magnitude < MinSlideSteer)
                groundMovement = horizontalVelocity.normalized;

            var targetVelocity = groundMovement * currentSpeed;
            var steerForce = (targetVelocity - horizontalVelocity) * steerAcceleration * deltaTime;

            horizontalVelocity += steerForce;
            horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, currentSpeed);

            return new Vector3(horizontalVelocity.x, groundAdjustment.y, horizontalVelocity.z);
        }

        private static Vector3 AirMovementForce(
            Vector3 planarVelocity,
            Vector3 requestedMovement,
            SurfaceProperties surface,
            float deltaTime)
        {
            var movementForce = requestedMovement * (surface.AirAcceleration * deltaTime);

            if (planarVelocity.magnitude < surface.AirSpeed)
            {
                var targetPlanarVelocity = Vector3.ClampMagnitude(planarVelocity + movementForce, surface.AirSpeed);
                return targetPlanarVelocity - planarVelocity;
            }

            if (Vector3.Dot(planarVelocity, movementForce) > 0f)
                return Vector3.ProjectOnPlane(movementForce, planarVelocity.normalized);

            return movementForce;
        }
    }
}
