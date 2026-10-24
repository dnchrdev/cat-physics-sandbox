using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Features.GameBootstrap.UseCases.Ports;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Features.GameBootstrap.InterfaceAdapters
{
    public class CommonAssetsHolder: ICommonAssetsHolder, IDisposable
    {
        private AsyncOperationHandle<IList<Object>> _handle;

        public async UniTask LoadAsync(CancellationToken ct)
        {
            await Addressables.InitializeAsync().ToUniTask(cancellationToken: ct);
            _handle = Addressables.LoadAssetsAsync<Object>("common");
            await _handle.ToUniTask(cancellationToken: ct);
        }

        public void Dispose()
        {
            if (_handle.IsValid())
                Addressables.Release(_handle);
        }
    }
}