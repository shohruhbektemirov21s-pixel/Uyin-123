using UnityEngine;
using BlockBlast.Save;
using BlockBlast.Utils;
namespace BlockBlast.Audio {
    public class AudioManager : MonoBehaviour {
        public static AudioManager Instance { get; private set; }
        private ObjectPool<AudioSource> _pool;
        public void Initialize() {
            Instance = this;
            _pool = new ObjectPool<AudioSource>(8, 
                () => new GameObject("AudioSrc").AddComponent<AudioSource>(),
                s => s.gameObject.SetActive(true), 
                s => s.gameObject.SetActive(false));
        }
        public void PlayTone(float frequency, float duration) {
            if (!SaveManager.Instance.Data.sfxEnabled) return;
            var src = _pool.Get();
            var clip = AudioClip.Create("Tone", (int)(44100 * duration), 1, 44100, false);
            float[] data = new float[clip.samples];
            for (int i = 0; i < data.Length; i++) data[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / 44100) * 0.5f;
            clip.SetData(data, 0);
            src.clip = clip; src.Play();
            StartCoroutine(ReturnToPool(src, duration));
        }
        public void PlayPlace() => PlayTone(400, 0.1f);
        public void PlayClear() => PlayTone(800, 0.2f);
        public void PlayInvalid() => PlayTone(150, 0.1f);
        public void PlayGameOver() => PlayTone(100, 0.5f);
        private System.Collections.IEnumerator ReturnToPool(AudioSource s, float delay) {
            yield return new WaitForSeconds(delay);
            _pool.Release(s); Destroy(s.clip);
        }
    }
}
