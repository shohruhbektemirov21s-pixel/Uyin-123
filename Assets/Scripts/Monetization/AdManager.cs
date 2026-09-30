using UnityEngine;
namespace BlockBlast.Monetization {
    public class AdManager : MonoBehaviour {
        public static AdManager Instance { get; private set; }
        private IAdProvider _provider;

        public void Initialize() {
            Instance = this;
#if BB_ADMOB
            _provider = new AdMobAdProvider();
#else
            _provider = new MockAdProvider();
#endif
            _provider.Initialize();
        }

        public void ShowInterstitial(System.Action onClosed) {
            if (_provider.IsInterstitialReady()) _provider.ShowInterstitial(onClosed);
            else onClosed?.Invoke();
        }
    }
}
