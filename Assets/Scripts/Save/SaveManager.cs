using UnityEngine;
namespace BlockBlast.Save {
    public class SaveManager : MonoBehaviour {
        public static SaveManager Instance { get; private set; }
        public SaveData Data { get; private set; }
        private const string KEY = "BB_Save";
        private bool _dirty;
        private float _timer;

        public void Initialize() {
            Instance = this;
            string json = PlayerPrefs.GetString(KEY, "");
            Data = string.IsNullOrEmpty(json) ? new SaveData() : JsonUtility.FromJson<SaveData>(json) ?? new SaveData();
        }
        public void MarkDirty() => _dirty = true;
        private void Update() {
            if (_dirty && (_timer += Time.deltaTime) > 1f) Flush();
        }
        private void Flush() {
            if (!_dirty) return;
            PlayerPrefs.SetString(KEY, JsonUtility.ToJson(Data));
            PlayerPrefs.Save();
            _dirty = false; _timer = 0f;
        }
        private void OnApplicationPause(bool p) { if (p) Flush(); }
        private void OnApplicationQuit() => Flush();
    }
}
