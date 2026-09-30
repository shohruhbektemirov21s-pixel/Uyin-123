using UnityEngine;
namespace BlockBlast.UI {
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaFitter : MonoBehaviour {
        private void Awake() {
            var rt = GetComponent<RectTransform>();
            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            rt.anchorMin = anchorMin;
            rt.anchorMax = anchorMax;
        }
    }
}
