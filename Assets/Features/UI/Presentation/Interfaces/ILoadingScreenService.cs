using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace Feature.UI
{
    public interface ILoadingScreenService
    {
        UniTask StartLoadingAsync();
        UniTask EndLoadingAsync();
    }
}