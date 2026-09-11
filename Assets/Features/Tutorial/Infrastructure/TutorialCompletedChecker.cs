using Features.Player.Domain.Interfeces;
using Features.UI.Infrastructure;
using Features.UI.Infrastructure.Data;
using UnityEngine;
using Zenject;

namespace Features.Tutorial.Infrastructure
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
