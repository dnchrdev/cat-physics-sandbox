using Feature.Core;
using Feature.UI;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Feature.Tutorial
{
    public class TutorialCompletedChecker : MonoBehaviour, IInitializable
    {
        [SerializeField] private Transform _finishAreaCenter;
        [SerializeField] private float _radius;

        private UIPanelsManager _panelsManager;
        private IWorldEntityService _worldEntityService;
        private GameObject _player;
        private bool _done = false;

        [Inject]
        private void Construct(IWorldEntityService worldEntityService, UIPanelsManager panelsManager)
        {
            _worldEntityService = worldEntityService;
            _panelsManager = panelsManager;

            _done = false;
        }

        public void Initialize()
        {
            var entiry = _worldEntityService.GetFirstEntityByTeam(Shared.TeamType.Player);
            _player = _worldEntityService.GetObjectByEntity(entiry);
        }

        private void Update()
        {
            if(_done) return;

            float distance = (_player.transform.position - _finishAreaCenter.position).magnitude;

            if (distance < _radius)
            {
                _done = true;
                _panelsManager.OpenPanel(UIPanelTag.TutorialCompleted);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_finishAreaCenter.position, _radius);
        }
    }
}
