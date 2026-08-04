using System;
using YG;
using Zenject;

namespace Feature.Advertising
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
