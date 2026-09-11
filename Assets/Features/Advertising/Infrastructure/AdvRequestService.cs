using System;
using Features.Advertising.Infrastructure.Interfaces;
using YG;

namespace Features.Advertising.Infrastructure
{
    public class AdvRequestService : IAdvRequestService
    {
        public void ShowInterstitial()
        {
            YG2.InterstitialAdvShow();
        }

        public void RewardedAdvRequest(Action callback)
        {
            YG2.RewardedAdvShow("", callback);
        }
        
    }
}
