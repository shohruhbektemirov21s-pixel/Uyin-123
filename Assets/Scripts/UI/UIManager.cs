using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BlockBlast.Core;

namespace BlockBlast.UI {
    public class UIManager : MonoBehaviour {
        public static UIManager Instance { get; private set; }
        private GameObject _menuScreen, _hudScreen, _gameOverScreen;
        private Text _hudScore, _hudBest, _goScore, _goBest;

        public void Initialize() {
            Instance = this;
            CreateCanvas();
            EnsureEventSystem();
            GameManager.Instance.OnStateChanged += UpdateScreens;
            GameManager.Instance.OnScoreChanged += UpdateHUD;
            UpdateScreens(GameManager.Instance.State);
            UpdateHUD();
        }

        private void CreateCanvas() {
            var go = new GameObject("UICanvas");
            var canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = go.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            scaler.matchWidthOrHeight = 1.0f;
            go.AddComponent<GraphicRaycaster>();

            _menuScreen = CreateScreen(go.transform, "MenuScreen");
            var startBtn = CreateButton(_menuScreen.transform, "StartButton", "PLAY", new Vector2(0, -200));
            startBtn.OnClick = () => GameManager.Instance.StartNewGame();

            _hudScreen = CreateScreen(go.transform, "HUDScreen");
            _hudScore = CreateText(_hudScreen.transform, "Score", new Vector2(0, 800), 100);
            _hudScore.gameObject.AddComponent<ScoreCounter>();
            _hudBest = CreateText(_hudScreen.transform, "Best", new Vector2(0, 700), 50);

            _gameOverScreen = CreateScreen(go.transform, "GameOverScreen");
            CreateText(_gameOverScreen.transform, "GameOverTitle", new Vector2(0, 400), 120, "GAME OVER");
            _goScore = CreateText(_gameOverScreen.transform, "Score: 0", new Vector2(0, 200), 80);
            _goBest = CreateText(_gameOverScreen.transform, "Best: 0", new Vector2(0, 100), 80);
            var retryBtn = CreateButton(_gameOverScreen.transform, "RetryBtn", "RETRY", new Vector2(0, -200));
            retryBtn.OnClick = () => GameManager.Instance.StartNewGame();
            var menuBtn = CreateButton(_gameOverScreen.transform, "MenuBtn", "MENU", new Vector2(0, -400));
            menuBtn.OnClick = () => GameManager.Instance.GoToMenu();
        }

        private void EnsureEventSystem() {
            if (FindFirstObjectByType<EventSystem>() != null) return;
            var go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();
        }

        private GameObject CreateScreen(Transform parent, string name) {
            var go = new GameObject(name);
            var rt = go.AddComponent<RectTransform>();
            rt.SetParent(parent, false);
            rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one;
            rt.sizeDelta = Vector2.zero;
            go.AddComponent<SafeAreaFitter>();
            return go;
        }

        private AnimatedButton CreateButton(Transform parent, string name, string text, Vector2 pos) {
            var go = new GameObject(name);
            var rt = go.AddComponent<RectTransform>();
            rt.SetParent(parent, false);
            rt.anchoredPosition = pos;
            rt.sizeDelta = new Vector2(400, 150);
            var img = go.AddComponent<Image>();
            img.color = Color.white;
            var btn = go.AddComponent<Button>();
            var txtGo = new GameObject("Text");
            txtGo.transform.SetParent(go.transform, false);
            var txt = txtGo.AddComponent<Text>();
            txt.text = text; txt.fontSize = 60; txt.color = Color.black; txt.alignment = TextAnchor.MiddleCenter;
            txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            var rtTxt = txtGo.GetComponent<RectTransform>();
            rtTxt.anchorMin = Vector2.zero; rtTxt.anchorMax = Vector2.one; rtTxt.sizeDelta = Vector2.zero;
            var animBtn = go.AddComponent<AnimatedButton>();
            return animBtn;
        }

        private Text CreateText(Transform parent, string name, Vector2 pos, int size, string initialText = "") {
            var go = new GameObject(name);
            var rt = go.AddComponent<RectTransform>();
            rt.SetParent(parent, false);
            rt.anchoredPosition = pos;
            rt.sizeDelta = new Vector2(800, size + 20);
            var txt = go.AddComponent<Text>();
            txt.text = initialText;
            txt.fontSize = size; txt.color = Color.white; txt.alignment = TextAnchor.MiddleCenter;
            txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            return txt;
        }

        private void UpdateScreens(GameState s) {
            _menuScreen.SetActive(s == GameState.Menu);
            _hudScreen.SetActive(s == GameState.Playing || s == GameState.Paused);
            _gameOverScreen.SetActive(s == GameState.GameOver);
            if (s == GameState.GameOver) {
                _goScore.text = "Score: " + GameManager.Instance.Score;
                _goBest.text = "Best: " + GameManager.Instance.HighScore;
            }
        }

        private void UpdateHUD() {
            _hudBest.text = "Best: " + GameManager.Instance.HighScore;
        }
    }
}
