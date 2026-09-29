using System.Collections.Generic;
using Features.Interaction.Entities;
using Features.Player.UseCases.Ports;

namespace Features.Interaction.UseCases
{
    public sealed class InteractableResetUseCase : IInteractableReset
    {
        private readonly List<InteractableEntity> _items = new();

        public void Register(InteractableEntity item)
        {
            if (item.IsRagdoll || _items.Contains(item)) return;

            _items.Add(item);
            item.SaveState();
        }

        public void ResetItems()
        {
            foreach (var item in _items)
                item.ResetState();
        }
    }
}
