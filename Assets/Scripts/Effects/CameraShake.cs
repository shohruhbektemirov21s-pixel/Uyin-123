using UnityEngine;
namespace BlockBlast.Effects {
    public class CameraShake : MonoBehaviour {
        public static CameraShake Instance { get; private set; }
        private float _duration;
        private float _magnitude;
        private Vector3 _originalPos;

        public void Initialize() {
            Instance = this;
            _originalPos = Camera.main.transform.localPosition;
        }

        public void Shake(float duration, float magnitude) {
            _duration = duration;
            _magnitude = magnitude;
        }

        private void Update() {
            if (_duration > 0) {
                Camera.main.transform.localPosition = _originalPos + (Vector3)Random.insideUnitCircle * _magnitude;
                _duration -= Time.deltaTime;
                if (_duration <= 0) Camera.main.transform.localPosition = _originalPos;
            }
        }
    }
}
