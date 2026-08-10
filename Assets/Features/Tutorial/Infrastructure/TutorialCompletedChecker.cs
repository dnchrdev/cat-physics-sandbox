using System;
using Feature.Core;
using Feature.PlayerFeature;
using Feature.UI;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Feature.Tutorial
{
    public class TutorialCompletedChecker : MonoBehaviour
    {
        [SerializeField] private Transform _finishAreaCenter;
        [SerializeField] private float _radius;

        private UIPanelsManager _panelsManager;
        private IReadOnlyPlayer _player;
        private bool _done = false;

        [Inject]
        private void Construct(IReadOnlyPlayer player, UIPanelsManager panelsManager)
        {
            _player = player;
            _panelsManager = panelsManager;
        }

        private void Awake()
        {
            _done = false;
        }
        
        private void Update()
        {
            if(_done) return;

            float distance = (_player.Position - _finishAreaCenter.position).magnitude;

            if (distance < _radius)
            {
                _done = true;
                _panelsManager.OpenPanel(PanelMode.TutorialCompleted);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_finishAreaCenter.position, _radius);
        }
    }
}
