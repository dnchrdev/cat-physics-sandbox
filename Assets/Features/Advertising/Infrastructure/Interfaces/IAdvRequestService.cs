using System;
using System.Collections;
using UnityEngine;

namespace Feature.Advertising
{
    public interface IAdvRequestService
    {
        void RewardedAdvRequest(Action callback);

        void ShowInterstitial();
    }
}