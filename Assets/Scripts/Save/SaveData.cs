using System;
namespace BlockBlast.Save {
    [Serializable]
    public class SaveData {
        public int highScore = 0;
        public int coins = 0;
        public bool sfxEnabled = true;
        public bool musicEnabled = true;
        public bool hapticsEnabled = true;
        public float sfxVolume = 1.0f;
        public float musicVolume = 1.0f;
        public string lastDailyRewardDate = "";
        public string achievementsJson = "";
        public string statisticsJson = "";
    }
}
