using Features.Advertising.UseCases;
using UnityEngine;
using Zenject;

namespace Features.Advertising.InterfaceAdapters.Controllers
{
    public sealed class InterstitialAdvController : ITickable
    {
        private readonly InterstitialAdvUseCase _interstitialAdv;

        public InterstitialAdvController(InterstitialAdvUseCase interstitialAdv)
        {
            _interstitialAdv = interstitialAdv;
        }

        public void Tick() => _interstitialAdv.Execute(Time.deltaTime);
    }
}
