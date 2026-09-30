using UnityEngine;
using BlockBlast.Save;
namespace BlockBlast.Audio {
    public static class HapticFeedback {
#if UNITY_ANDROID && !UNITY_EDITOR
        private static AndroidJavaObject _vibrator;
#endif
        public static void Initialize() {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                _vibrator = activity.Call<AndroidJavaObject>("getSystemService", "vibrator");
            }
#endif
        }
        public static void Light() => Vibrate(10);
        public static void Heavy() => Vibrate(30);
        private static void Vibrate(long ms) {
            if (!SaveManager.Instance.Data.hapticsEnabled) return;
#if UNITY_ANDROID && !UNITY_EDITOR
            _vibrator?.Call("vibrate", ms);
#endif
        }
    }
}
