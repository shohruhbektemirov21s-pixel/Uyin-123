using UnityEngine;
namespace BlockBlast.Grid {
    public class BlockSpawner : MonoBehaviour {
        public static BlockSpawner Instance { get; private set; }
        private int _blocksRemaining;
        public void Initialize() { Instance = this; }
        public void ClearBlocks() {
            foreach (var block in FindObjectsByType<DraggableBlock>(FindObjectsSortMode.None)) {
                Destroy(block.gameObject);
            }
            _blocksRemaining = 0;
        }
        public void SpawnBatch(BlockShape[] shapes) {
            _blocksRemaining = shapes.Length;
            for (int i = 0; i < shapes.Length; i++) {
                var go = new GameObject($"Block_{i}");
                go.transform.position = new Vector3(0.5f + i * 3, -3f, 0);
                go.AddComponent<DraggableBlock>().Setup(shapes[i]);
            }
        }
        public void OnBlockPlaced() {
            _blocksRemaining--;
            if (_blocksRemaining <= 0) {
                BlockBlast.Gameplay.ShapeGenerator.Instance.GenerateNextBatch();
            } else {
                BlockBlast.Gameplay.ShapeGenerator.Instance.CheckGameOver();
            }
        }
    }
}
