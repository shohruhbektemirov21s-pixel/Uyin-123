using UnityEngine;
using UnityEngine.UI;
using BlockBlast.Core;

namespace BlockBlast.UI {
    [RequireComponent(typeof(Text))]
    public class ScoreCounter : MonoBehaviour {
        private Text _text;
        private int _currentDisplayed;
        private void Awake() {
            _text = GetComponent<Text>();
            GameManager.Instance.OnScoreChanged += () => { enabled = true; };
        }
        private void Update() {
            int target = GameManager.Instance.Score;
            if (_currentDisplayed != target) {
                _currentDisplayed = (int)Mathf.MoveTowards(_currentDisplayed, target, Time.deltaTime * Mathf.Max(100, target - _currentDisplayed));
                _text.text = _currentDisplayed.ToString();
            } else {
                enabled = false;
            }
        }
    }
}
