using UnityEngine;
using UnityEngine.UI;

namespace BlockBlast.Effects {
    public class FloatingPopup : MonoBehaviour {
        private float _lifetime;
        private Text _text;
        
        public void Setup(string txt, Color color) {
            if (_text == null) _text = gameObject.AddComponent<Text>();
            _text.text = txt;
            _text.color = color;
            _lifetime = 1.0f;
        }

        private void Update() {
            transform.position += Vector3.up * Time.deltaTime;
            _lifetime -= Time.deltaTime;
            if (_lifetime <= 0) {
                gameObject.SetActive(false);
                EffectsManager.Instance.ReturnPopup(this);
            }
        }
    }
}
