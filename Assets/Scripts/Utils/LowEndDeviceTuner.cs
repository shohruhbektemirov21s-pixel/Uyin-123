using UnityEngine;
namespace BlockBlast.Utils {
    public static class LowEndDeviceTuner {
        public static bool IsLowEnd { get; private set; }
        public static void Apply() {
            IsLowEnd = SystemInfo.systemMemorySize > 0 && SystemInfo.systemMemorySize < 3000 || SystemInfo.processorCount <= 4;
            Application.targetFrameRate = IsLowEnd ? 30 : 60;
            QualitySettings.vSyncCount = 0;
            if (IsLowEnd) { QualitySettings.masterTextureLimit = 1; QualitySettings.particleRaycastBudget = 16; }
        }
    }
}
