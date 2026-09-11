using System;
using System.Collections.Generic;
using Features.PhysicsInteraction.Infrastructure.Interfaces;

namespace Features.PhysicsInteraction.Application
{
    public class InteractableResetService: IDisposable
    {
        private List<IResetable> _resetables = new();

        public void AddItem(IResetable resetable)
        {
            if(_resetables.Contains(resetable)) return;

            _resetables.Add(resetable);
            resetable.SaveState();
        }

        public void ResetItems()
        {
            foreach (var item in _resetables)
            {
                item.ResetState();
            }
        }

        public void Dispose()
        {
            _resetables.Clear();
        }
    }

 }