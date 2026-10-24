using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Features.GameBootstrap.UseCases;
using Zenject;

namespace Features.GameBootstrap.InterfaceAdapters
{
    public sealed class GameBootstrapController : IInitializable, IDisposable
    {
        private readonly GameLaunchUseCase _gameLaunch;

        private CancellationTokenSource _cts;

        public GameBootstrapController(GameLaunchUseCase gameLaunch)
        {
            _gameLaunch = gameLaunch;
        }

        public void Initialize()
        {
            _cts =  new CancellationTokenSource();
            _gameLaunch.LaunchAsync(_cts.Token).Forget();
        }

        public void Dispose()
        {
            _cts.Cancel();
        }
    }
}
