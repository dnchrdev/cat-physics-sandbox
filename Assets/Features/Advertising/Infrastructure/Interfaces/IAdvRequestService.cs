using System;

namespace Features.Advertising.Infrastructure.Interfaces
{
    public interface IAdvRequestService
    {
        void RewardedAdvRequest(Action callback);

        void ShowInterstitial();
    }
}