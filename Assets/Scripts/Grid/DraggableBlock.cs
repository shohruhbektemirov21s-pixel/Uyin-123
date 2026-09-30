using UnityEngine;
using BlockBlast.Core;
using BlockBlast.Utils;

namespace BlockBlast.Grid {
    public class DraggableBlock : MonoBehaviour {
        public BlockShape Shape { get; private set; }
        private Vector3 _startPos;
        private bool _isDragging;
        private int _fingerId = -1;
        private static bool _globalDragLock;

        public void Setup(BlockShape shape) {
            Shape = shape;
            CreateVisuals();
        }

        private void Update() {
            if (GameManager.Instance.State != GameState.Playing) return;

            if (Input.touchCount > 0) {
                foreach (var touch in Input.touches) {
                    if (!_isDragging && !_globalDragLock && touch.phase == TouchPhase.Began) {
                        if (BeginDrag(touch.position)) _fingerId = touch.fingerId;
                    } else if (_isDragging && touch.fingerId == _fingerId) {
                        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                            ContinueDrag(touch.position);
                        } else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
                            EndDrag();
                        }
                    }
                }
            } else if (Input.GetMouseButtonDown(0)) {
                BeginDrag(Input.mousePosition);
            } else if (_isDragging && Input.GetMouseButton(0)) {
                ContinueDrag(Input.mousePosition);
            } else if (_isDragging && Input.GetMouseButtonUp(0)) {
                EndDrag();
            }
        }

        private void CreateVisuals() {
            for (int x = 0; x < Shape.Width; x++) {
                for (int y = 0; y < Shape.Height; y++) {
                    if (!Shape.Matrix[x, y]) continue;
                    var go = new GameObject($"Cell_{x}_{y}");
                    go.transform.SetParent(transform, false);
                    go.transform.localPosition = new Vector3(x, y, 0);
                    go.transform.localScale = Vector3.one * 0.9f;
                    var renderer = go.AddComponent<SpriteRenderer>();
                    renderer.sprite = SpriteFactory.CreateSquare();
                    renderer.color = Shape.Color;
                    renderer.sortingOrder = 10;
                }
            }
        }

        private bool BeginDrag(Vector2 screenPosition) {
            if (_isDragging || _globalDragLock) return false;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            if (!Contains(worldPosition)) return false;
            _isDragging = true;
            _globalDragLock = true;
            _startPos = transform.position;
            return true;
        }

        private bool Contains(Vector2 worldPosition) {
            Vector2 local = worldPosition - (Vector2)transform.position;
            for (int x = 0; x < Shape.Width; x++) {
                for (int y = 0; y < Shape.Height; y++) {
                    if (Shape.Matrix[x, y] && Mathf.Abs(local.x - x) <= 0.55f && Mathf.Abs(local.y - y) <= 0.55f) return true;
                }
            }
            return false;
        }

        private void ContinueDrag(Vector2 screenPosition) {
            Vector3 target = Camera.main.ScreenToWorldPoint(screenPosition);
            target.z = 0;
            target.y += 1.5f;
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 20f);
        }

        private void EndDrag() {
            _isDragging = false;
            _globalDragLock = false;
            _fingerId = -1;
            TryPlaceOnGrid();
        }

        private void TryPlaceOnGrid() {
            int gridX = Mathf.RoundToInt(transform.position.x);
            int gridY = Mathf.RoundToInt(transform.position.y);
            var result = GridManager.Instance.TryPlace(Shape, gridX, gridY);
            if (result.IsValid) {
                Destroy(gameObject);
                FindObjectOfType<BlockSpawner>()?.OnBlockPlaced();
            } else {
                transform.position = _startPos;
            }
        }
    }
}
