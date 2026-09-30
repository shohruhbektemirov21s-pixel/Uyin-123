using System;
using System.Collections.Generic;
using UnityEngine;
namespace BlockBlast.Save {
    [Serializable] public class AchievementData { public string id; public bool unlocked; public int progress; }
    [Serializable] public class AchievementListWrapper { public List<AchievementData> achievements = new List<AchievementData>(); }
    public class Achievements {
        private Dictionary<string, AchievementData> _data = new Dictionary<string, AchievementData>();
        public void Load(string json) {
            _data.Clear();
            if (!string.IsNullOrEmpty(json)) {
                try {
                    var w = JsonUtility.FromJson<AchievementListWrapper>(json);
                    foreach (var ach in w.achievements) _data[ach.id] = ach;
                } catch { }
            }
        }
        public string Save() {
            var w = new AchievementListWrapper();
            foreach (var kvp in _data) w.achievements.Add(kvp.Value);
            return JsonUtility.ToJson(w);
        }
        public void Unlock(string id) {
            if (!_data.ContainsKey(id)) _data[id] = new AchievementData { id = id };
            if (!_data[id].unlocked) { _data[id].unlocked = true; SaveManager.Instance.MarkDirty(); }
        }
    }
}
