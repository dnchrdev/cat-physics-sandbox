using Feature.Core;
using UnityEngine;
using Zenject;

namespace Feature.PlayerFeature
{
    public class PlayerBinder : MonoBehaviour
    {
        private EntityWorldBind _entityWorldBind;
        private Player _player;
        private IWorldEntityService _worldEntityService;

        [Inject]
        private void Construct(Player player, IWorldEntityService worldEntityService)
        {
            _entityWorldBind = GetComponent<EntityWorldBind>();
            _player = player;
            _entityWorldBind.Bind(player, player);
            _worldEntityService = worldEntityService;
            _worldEntityService.Bind(player, player, gameObject);
        }

        public void OnDisable()
        {
            _worldEntityService.Unbind(_player);
        }
    }
}