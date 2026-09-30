using System;
using BlockBlast.Utils;
namespace BlockBlast.Monetization {
    public class AdMobAdProvider : IAdProvider {
        public void Initialize() {
#if BB_ADMOB
            Log.Info("AdMob Initialize stub");
#endif
        }
        public bool IsInterstitialReady() => false;
        public void ShowInterstitial(Action onClosed) => onClosed?.Invoke();
        public bool IsRewardedReady() => false;
        public void ShowRewarded(Action onRewarded, Action onClosed) => onClosed?.Invoke();
    }
}
