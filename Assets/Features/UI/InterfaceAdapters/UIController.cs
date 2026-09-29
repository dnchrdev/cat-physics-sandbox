using Features.UI.UseCases;
using UnityEngine;
using Zenject;

namespace Features.UI.InterfaceAdapters
{
    public sealed class UIController : ITickable
    {
        private readonly UIPanelsManager _panelsManager;

        public UIController(UIPanelsManager panelsManager)
        {
            _panelsManager = panelsManager;
        }

        public void Tick() => _panelsManager.Tick(Time.deltaTime);
    }
}
