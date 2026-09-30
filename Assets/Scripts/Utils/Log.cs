using UnityEngine;
namespace BlockBlast.Utils {
    public static class Log {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        private const bool Enabled = true;
#else
        private const bool Enabled = false;
#endif
        public static void Info(string msg) { if (Enabled) Debug.Log($"[BlockBlast] {msg}"); }
        public static void Warn(string msg) { if (Enabled) Debug.LogWarning($"[BlockBlast] {msg}"); }
        public static void Error(string msg) { if (Enabled) Debug.LogError($"[BlockBlast] {msg}"); }
    }
}
