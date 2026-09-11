using Features.Core.Infrastructure.Binder;
using Features.Core.Infrastructure.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace Features.Player.Insrastructure
{
    public class PlayerBinder : MonoBehaviour
    {
        private EntityWorldBind _entityWorldBind;
        private Domain.Player _player;
        private IWorldEntityService _worldEntityService;

        [Inject]
        private void Construct(Domain.Player player, IWorldEntityService worldEntityService)
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