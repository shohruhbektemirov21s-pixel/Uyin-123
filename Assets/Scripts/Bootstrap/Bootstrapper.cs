using UnityEngine;
using BlockBlast.Core;
using BlockBlast.Grid;
using BlockBlast.UI;
using BlockBlast.Effects;
using BlockBlast.Gameplay;
using BlockBlast.Audio;
using BlockBlast.Save;
using BlockBlast.Cosmetics;
using BlockBlast.Monetization;
using BlockBlast.Utils;

namespace BlockBlast.Bootstrap {
    public class Bootstrapper : MonoBehaviour {
        private void Awake() {
            Application.targetFrameRate = 60;
            LowEndDeviceTuner.Apply();
            HapticFeedback.Initialize();

            Camera.main.orthographicSize = 8;
            Camera.main.transform.position = new Vector3(3.5f, 3.5f, -10);
            
            gameObject.AddComponent<SaveManager>().Initialize();
            gameObject.AddComponent<ThemeManager>().Initialize();
            gameObject.AddComponent<AudioManager>().Initialize();
            gameObject.AddComponent<AdManager>().Initialize();
            gameObject.AddComponent<GameManager>().Initialize();
            gameObject.AddComponent<GridManager>().Initialize();
            gameObject.AddComponent<BlockSpawner>().Initialize();
            gameObject.AddComponent<ShapeGenerator>().Initialize();
            gameObject.AddComponent<EffectsManager>().Initialize();
            gameObject.AddComponent<CameraShake>().Initialize();
            gameObject.AddComponent<UIManager>().Initialize();
            
            var go = new GameObject("GridDebugger");
            go.AddComponent<GridDebugger>();
        }
    }
}
