using System.Collections;
using UnityEngine;

namespace Feature.CameraFeature
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "SO/CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        [field: Header("CameraPivot")]
        [field: SerializeField, Range(-89, 0)] public float MinPitchLimit { get; private set; }
        [field: SerializeField, Range(0, 89)] public float MaxPitchLimit { get; private set; }
        [field: SerializeField, Range(0, 100)] public float MoveSpeedSmoothing { get; private set; }
        [field: SerializeField, Range(0, 100)] public float TurnSpeedSmoothing { get; private set; }
        [field: SerializeField, Range(0, 100)] public float DeadPlayerTurnSpeedResponse { get; private set; }
        
        [field: Header("Headbob")]
        [field: SerializeField] public float BobSpeedMin { get; private set; } = 0f;
        [field: SerializeField] public float BobAmplitudeMin { get; private set; } = 0.01f;
        [field: SerializeField] public float BobFrequencyMin { get; private set; } = 0.8f;

        [field: SerializeField] public float BobSpeedMax { get; private set; } = 6.0f;
        [field: SerializeField] public float BobAmplitudeMax { get; private set; } = 0.05f;
        [field: SerializeField] public float BobFrequencyMax { get; private set; } = 2.5f;

        [field: SerializeField] public float BobHorizontalMultiplier { get; private set; } = 0.5f;
        [field: SerializeField] public float BobAmplitudeResponse { get; private set; } = 8.0f;
        [field: SerializeField] public float BobFrequencyResponse { get; private set; } = 4.0f;
        [field: SerializeField] public float BobDecayDamping { get; private set; } = 0.12f;

        [field: SerializeField] public float BobSlideAmplitude { get; private set; } = 0.02f;
        [field: SerializeField] public float BobSlideFrequency { get; private set; } = 0.5f;

        [field: Header("CameraLean")]
        [field: SerializeField] public float AttackDamping { get; private set; } = 0.5f;

        [field: SerializeField] public float DecayDamping { get; private set; } = 0.3f;
        [field: SerializeField] public float MoveStrength { get; private set; } = 0.5f;
        [field: SerializeField] public float SlideStrength { get; private set; } = 1f;
        [field: SerializeField] public float StrengthResponse { get; private set; } = 1f;


    }

}