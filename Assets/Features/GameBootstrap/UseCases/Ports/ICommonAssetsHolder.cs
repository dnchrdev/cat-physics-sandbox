using System.Threading;
using Cysharp.Threading.Tasks;

namespace Features.GameBootstrap.UseCases.Ports
{
    public interface ICommonAssetsHolder
    {
        public async UniTask LoadAsync(CancellationToken ct) {}
    }
}