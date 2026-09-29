using Features.Core.Infrastructure;
using UnityEngine;
using Zenject;

namespace Features.Player.Infrastructure
{
    public class PlayerBinder : MonoBehaviour
    {
        private Entities.Player _player;

        [Inject]
        private void Construct(Entities.Player player)
        {
            _player = player;
            GetComponent<EntityWorldBind>().Bind(player, player);
        }
        
    }
}
