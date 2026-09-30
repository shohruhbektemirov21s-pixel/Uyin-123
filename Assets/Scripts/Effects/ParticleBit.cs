using UnityEngine;

namespace BlockBlast.Effects {
    public class ParticleBit : MonoBehaviour {
        private Vector3 _velocity;
        private float _lifetime;
        
        public void Setup(Color color, Vector2 vel) {
            _velocity = vel;
            _lifetime = 1.0f;
            GetComponent<SpriteRenderer>().color = color;
        }

        private void Update() {
            transform.position += _velocity * Time.deltaTime;
            _velocity.y -= 9.8f * Time.deltaTime; // Gravity
            _lifetime -= Time.deltaTime;
            if (_lifetime <= 0) {
                gameObject.SetActive(false);
                EffectsManager.Instance.ReturnParticle(this);
            }
        }
    }
}
