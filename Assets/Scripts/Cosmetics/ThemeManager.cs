using UnityEngine;
using BlockBlast.Save;

namespace BlockBlast.Cosmetics {
    public class ThemeManager : MonoBehaviour {
        public static ThemeManager Instance { get; private set; }
        public Theme ActiveTheme { get; private set; }
        public void Initialize() {
            Instance = this;
            ActiveTheme = new Theme {
                id = "default",
                blockColors = new Color[] { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta },
                backgroundColor = new Color(0.1f, 0.1f, 0.1f),
                gridColor = new Color(0.2f, 0.2f, 0.2f)
            };
            Camera.main.backgroundColor = ActiveTheme.backgroundColor;
        }
    }
}
