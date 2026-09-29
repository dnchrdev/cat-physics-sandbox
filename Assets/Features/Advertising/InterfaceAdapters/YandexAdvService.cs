using System;
using Features.Advertising.UseCases.Ports;
using Features.Shared.UseCases;
using YG;

namespace Features.Advertising.InterfaceAdapters
{
    public sealed class YandexAdvService : IInterstitialAdvGateway, IRewardedAdvRequest
    {
        public void ShowInterstitial() => YG2.InterstitialAdvShow();

        public void ShowRewarded(Action onRewarded) => YG2.RewardedAdvShow("", onRewarded);
    }
}
