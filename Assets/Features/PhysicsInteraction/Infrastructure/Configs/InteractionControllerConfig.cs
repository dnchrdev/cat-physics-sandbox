using System.Collections;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    [CreateAssetMenu(fileName = "InteractionControllerConfig", menuName = "SO/InteractionControllerConfig")]
        public class InteractionControllerConfig : ScriptableObject
    {
        [field: SerializeField] public float FocusDistance { get; private set; } = 0.5f;
        [field: SerializeField] public float FocusSphereCastRadius { get; private set; } = 0.25f;
        [field: SerializeField] public float MinDistanceBetweenPlayerAndInteractableMesh { get; internal set; } = 0.25f;
        [field: SerializeField] public float ReleaseCooldown { get; private set; } = 1f;
        [field: SerializeField] public float ForceGrabbedReleaseDistance { get; private set; } = 5f;
        [field: SerializeField] public float GrabbedThrowPower { get; private set; } = 5f;
        [field: SerializeField] public float FocusHitPower { get; private set; } = 2f;
        [field: SerializeField] public LayerMask InteractionMask { get; private set; }
        [field: SerializeField] public LayerMask AimingPointMask { get; private set; }
        [field: SerializeField] public float IndicationScreenTranslationResponse { get; private set; } = 10f;
    }
}