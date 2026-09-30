using UnityEngine;
using BlockBlast.Core;
using BlockBlast.Grid;

namespace BlockBlast.Gameplay {
    public class ShapeGenerator : MonoBehaviour {
        public static ShapeGenerator Instance { get; private set; }
        private GameState _previousState;
        
        public void Initialize() {
            Instance = this;
            _previousState = GameManager.Instance.State;
            GameManager.Instance.OnStateChanged += HandleStateChanged;
        }

        private void HandleStateChanged(GameState state) {
            if (state == GameState.Playing && _previousState != GameState.Paused) {
                BlockSpawner.Instance.ClearBlocks();
                GenerateNextBatch();
            }
            _previousState = state;
        }

        public void GenerateNextBatch() {
            BlockShape[] batch = new BlockShape[3];
            for (int i = 0; i < 3; i++) {
                batch[i] = BlockShapeLibrary.Shapes[Random.Range(0, BlockShapeLibrary.Shapes.Length)];
            }
            
            BlockSpawner.Instance.SpawnBatch(batch);
            CheckGameOver();
        }

        public void CheckGameOver() {
            if (GameManager.Instance.State != GameState.Playing) return;
            var blocks = FindObjectsOfType<DraggableBlock>();
            if (blocks.Length == 0) return;

            foreach (var b in blocks) {
                for (int x = 0; x < 8; x++) {
                    for (int y = 0; y < 8; y++) {
                        if (GridManager.Instance.CanPlace(b.Shape, x, y)) return; // Still playable
                    }
                }
            }
            GameManager.Instance.TriggerGameOver();
        }
    }
}
