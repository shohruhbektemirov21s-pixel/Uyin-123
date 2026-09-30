using System;
using UnityEngine;
using BlockBlast.Save;

namespace BlockBlast.Core {
    public enum GameState { Menu, Playing, Paused, GameOver }
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }
        public GameState State { get; private set; }
        public int Score { get; private set; }
        public int HighScore { get; private set; }
        public int Combo { get; private set; }
        public float Multiplier { get; private set; } = 1.0f;
        public int Level { get; private set; } = 1;

        public event Action<GameState> OnStateChanged;
        public event Action OnScoreChanged;
        public event Action OnPlacementResolved;

        public void Initialize() {
            Instance = this;
            HighScore = SaveManager.Instance.Data.highScore;
            SetState(GameState.Menu);
        }

        public void StartNewGame() {
            Score = 0; Combo = 0; Multiplier = 1.0f; Level = 1;
            SetState(GameState.Playing);
            OnScoreChanged?.Invoke();
        }

        public void Pause() { if (State == GameState.Playing) { Time.timeScale = 0; SetState(GameState.Paused); } }
        public void Resume() { if (State == GameState.Paused) { Time.timeScale = 1; SetState(GameState.Playing); } }
        public void GoToMenu() { Time.timeScale = 1; SetState(GameState.Menu); }
        public void TriggerGameOver() {
            if (Score > HighScore) {
                HighScore = Score;
                SaveManager.Instance.Data.highScore = HighScore;
                SaveManager.Instance.MarkDirty();
            }
            SetState(GameState.GameOver);
        }

        private void SetState(GameState s) {
            State = s;
            OnStateChanged?.Invoke(s);
        }

        public void RegisterPlacement(int cells, int linesCleared, int simultaneousClears) {
            if (linesCleared > 0) {
                Combo++;
                Multiplier = Mathf.Min(3.0f, Multiplier + 0.5f);
                if (Combo % 3 == 0) Level = Mathf.Min(10, Level + 1);
            } else {
                Combo = 0;
                Multiplier = 1.0f;
            }
            int baseScore = cells * 10;
            int lineBonus = linesCleared * 100;
            int simBonus = simultaneousClears > 1 ? (simultaneousClears * 50) : 0;
            Score += Mathf.RoundToInt((baseScore + lineBonus + simBonus) * Multiplier);
            OnScoreChanged?.Invoke();
            OnPlacementResolved?.Invoke();
        }
    }
}
