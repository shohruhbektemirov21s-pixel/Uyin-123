using UnityEngine;
using BlockBlast.Core;

namespace BlockBlast.Gameplay {
    public static class DifficultyDirector {
        public static float LargeShapeBias(int level) => Mathf.Clamp01(0.20f + (level - 1) * 0.08f);
        public static float SmallShapeFloor(int level) => Mathf.Clamp(0.25f - (level - 1) * 0.015f, 0.10f, 0.35f);
        public static int GenerationAttempts(int level) => 12 + level * 2;
    }
}
