using System;
namespace BlockBlast.Monetization {
    public interface IAdProvider {
        void Initialize();
        bool IsInterstitialReady();
        void ShowInterstitial(Action onClosed);
        bool IsRewardedReady();
        void ShowRewarded(Action onRewarded, Action onClosed);
    }
}
