using System;
using UnityEngine;
using BlockBlast.Utils;
namespace BlockBlast.Monetization {
    public class MockAdProvider : IAdProvider {
        public void Initialize() => Log.Info("MockAdProvider Initialized");
        public bool IsInterstitialReady() => true;
        public void ShowInterstitial(Action onClosed) { Log.Info("Showing Mock Interstitial"); onClosed?.Invoke(); }
        public bool IsRewardedReady() => true;
        public void ShowRewarded(Action onRewarded, Action onClosed) {
            Log.Info("Showing Mock Rewarded");
            onRewarded?.Invoke();
            onClosed?.Invoke();
        }
    }
}
