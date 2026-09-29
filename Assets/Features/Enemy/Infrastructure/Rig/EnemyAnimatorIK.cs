using Features.Enemy.InterfaceAdapters.Ports;
using UnityEngine;

namespace Features.Enemy.Infrastructure.Rig
{
    public class EnemyAnimatorIK : MonoBehaviour, IHeadLookView
    {
        private Animator _animator;
        private Vector3 _target;
        private float _weight;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetLookAt(Vector3 target, float weight)
        {
            _target = target;
            _weight = weight;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            _animator.SetLookAtPosition(_target);
            _animator.SetLookAtWeight(_weight);
        }
    }
}
