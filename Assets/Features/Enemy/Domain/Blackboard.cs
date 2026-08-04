using UnityEngine;

namespace Feature.EnemyFeature
{
    public class Blackboard
    {
        public int CurrentPatrolingIndex;
        public int CurrentPatrolingReverseStrike;
        public Vector3 CurrentPatrolPosition;
        public float PatrolingDelay;
        public bool IsReversePatroling;

        //Pursuit
        public bool IsAngry;

        //Ragdoll
        public bool IsRagdoll;

        //HeadLook
        public float TimerHeadLooking;
        public bool IsHeadLooking;
    }
}