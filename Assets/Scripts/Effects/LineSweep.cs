using UnityEngine;

namespace BlockBlast.Effects {
    public class LineSweep : MonoBehaviour {
        private float _timer;
        private void Update() {
            _timer += Time.deltaTime;
            if (_timer > 0.5f) {
                _timer = 0;
                gameObject.SetActive(false);
                EffectsManager.Instance.ReturnLineSweep(this);
            }
        }
    }
}
