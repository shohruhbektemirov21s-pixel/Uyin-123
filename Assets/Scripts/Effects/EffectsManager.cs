using UnityEngine;
using BlockBlast.Utils;

namespace BlockBlast.Effects {
    public class EffectsManager : MonoBehaviour {
        public static EffectsManager Instance { get; private set; }
        private ObjectPool<ParticleBit> _particles;
        private ObjectPool<LineSweep> _sweeps;
        private ObjectPool<FloatingPopup> _popups;

        public void Initialize() {
            Instance = this;
            var ptContainer = new GameObject("Particles").transform;
            _particles = new ObjectPool<ParticleBit>(256, () => {
                var go = new GameObject("Bit");
                go.transform.SetParent(ptContainer);
                go.AddComponent<SpriteRenderer>().sprite = SpriteFactory.CreateSquare();
                return go.AddComponent<ParticleBit>();
            }, b => b.gameObject.SetActive(true), b => b.gameObject.SetActive(false));

            _sweeps = new ObjectPool<LineSweep>(8, () => new GameObject("Sweep").AddComponent<LineSweep>(), 
                s => s.gameObject.SetActive(true), s => s.gameObject.SetActive(false));
                
            _popups = new ObjectPool<FloatingPopup>(8, () => new GameObject("Popup").AddComponent<FloatingPopup>(),
                p => p.gameObject.SetActive(true), p => p.gameObject.SetActive(false));
        }

        public void SpawnParticles(Vector3 pos, Color color, int count) {
            if (LowEndDeviceTuner.IsLowEnd) count /= 2;
            for (int i = 0; i < count; i++) {
                var p = _particles.Get();
                p.transform.position = pos;
                p.Setup(color, Random.insideUnitCircle * 5f);
            }
        }

        public void ReturnParticle(ParticleBit p) => _particles.Release(p);
        public void ReturnLineSweep(LineSweep s) => _sweeps.Release(s);
        public void ReturnPopup(FloatingPopup p) => _popups.Release(p);
    }
}
