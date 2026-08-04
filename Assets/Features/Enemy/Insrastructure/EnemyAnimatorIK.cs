using Feature.PlayerFeature;
using Feature.UI;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Feature.EnemyFeature
{
    public class EnemyAnimatorIK : MonoBehaviour
    {
        private Animator _animator;
        private EnemyVisionAndLook _enemyVisionAndLook;
        private IReadOnlyPlayer _player;

        [Inject]
        private void Constrcut(Animator animator, EnemyVisionAndLook enemyVisionAndLook, IReadOnlyPlayer player)
        {
            _animator = animator;
            _enemyVisionAndLook = enemyVisionAndLook;
            _player = player;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            _animator.SetLookAtPosition(_player.Position);
            _animator.SetLookAtWeight(_enemyVisionAndLook.HeadLookWeight);
        }
    }
}