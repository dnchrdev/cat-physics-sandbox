namespace Features.Camera.Entities
{
    public sealed class CameraSettings
    {
        public RotationConfig Rotation { get; }
        public HeadbobConfig Headbob { get; }
        public LeanConfig Lean { get; }
        public float DeadPlayerTurnSpeedResponse { get; }

        public CameraSettings(RotationConfig rotation, HeadbobConfig headbob, LeanConfig lean, float deadPlayerTurnSpeedResponse)
        {
            Rotation = rotation;
            Headbob = headbob;
            Lean = lean;
            DeadPlayerTurnSpeedResponse = deadPlayerTurnSpeedResponse;
        }
    }
}
