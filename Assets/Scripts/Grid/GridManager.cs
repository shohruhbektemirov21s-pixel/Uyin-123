using UnityEngine;
using BlockBlast.Core;
using BlockBlast.Utils;
using System.Collections.Generic;

namespace BlockBlast.Grid {
    public class GridManager : MonoBehaviour {
        public static GridManager Instance { get; private set; }
        private Cell[,] _grid = new Cell[8, 8];
        private int[] _rowCounts = new int[8];
        private int[] _colCounts = new int[8];
        private SpriteRenderer[,] _cellRenderers = new SpriteRenderer[8, 8];
        private static readonly Color EmptyCellColor = new Color(0.18f, 0.18f, 0.23f, 1f);

        public void Initialize() {
            Instance = this;
            CreateGridVisuals();
            GameManager.Instance.OnStateChanged += s => { if (s == GameState.Playing) ClearGrid(); };
        }

        private void CreateGridVisuals() {
            var container = new GameObject("GridVisuals").transform;
            container.SetParent(transform, false);
            for (int x = 0; x < 8; x++) {
                for (int y = 0; y < 8; y++) {
                    var go = new GameObject($"Cell_{x}_{y}");
                    go.transform.SetParent(container, false);
                    go.transform.localPosition = new Vector3(x, y, 0);
                    go.transform.localScale = Vector3.one * 0.9f;
                    var renderer = go.AddComponent<SpriteRenderer>();
                    renderer.sprite = SpriteFactory.CreateSquare();
                    renderer.color = EmptyCellColor;
                    _cellRenderers[x, y] = renderer;
                }
            }
        }

        private void ClearGrid() {
            for (int x = 0; x < 8; x++) {
                _colCounts[x] = 0;
                _rowCounts[x] = 0;
                for (int y = 0; y < 8; y++) {
                    _grid[x, y] = new Cell();
                    _cellRenderers[x, y].color = EmptyCellColor;
                }
            }
        }

        public bool CanPlace(BlockShape shape, int startX, int startY) {
            if (startX < 0 || startY < 0 || startX + shape.Width > 8 || startY + shape.Height > 8) return false;
            for (int x = 0; x < shape.Width; x++) {
                for (int y = 0; y < shape.Height; y++) {
                    if (shape.Matrix[x, y] && _grid[startX + x, startY + y].IsOccupied) return false;
                }
            }
            return true;
        }

        public PlacementResult TryPlace(BlockShape shape, int startX, int startY) {
            if (!CanPlace(shape, startX, startY)) return new PlacementResult { IsValid = false };
            int cellsPlaced = 0;
            for (int x = 0; x < shape.Width; x++) {
                for (int y = 0; y < shape.Height; y++) {
                    if (shape.Matrix[x, y]) {
                        _grid[startX + x, startY + y] = new Cell { IsOccupied = true, Color = shape.Color };
                        _colCounts[startX + x]++;
                        _rowCounts[startY + y]++;
                        _cellRenderers[startX + x, startY + y].color = shape.Color;
                        cellsPlaced++;
                    }
                }
            }
            int lines = CheckAndClearLines();
            GameManager.Instance.RegisterPlacement(cellsPlaced, lines, lines);
            return new PlacementResult { IsValid = true, LinesCleared = lines };
        }

        private int CheckAndClearLines() {
            List<int> clearRows = new List<int>(8);
            List<int> clearCols = new List<int>(8);
            for (int i = 0; i < 8; i++) {
                if (_rowCounts[i] == 8) clearRows.Add(i);
                if (_colCounts[i] == 8) clearCols.Add(i);
            }
            foreach (int r in clearRows) {
                _rowCounts[r] = 0;
                for (int x = 0; x < 8; x++) {
                    if (_grid[x, r].IsOccupied) _colCounts[x]--;
                    _grid[x, r] = new Cell();
                    _cellRenderers[x, r].color = EmptyCellColor;
                }
            }
            foreach (int c in clearCols) {
                _colCounts[c] = 0;
                for (int y = 0; y < 8; y++) {
                    if (_grid[c, y].IsOccupied) _rowCounts[y]--;
                    _grid[c, y] = new Cell();
                    _cellRenderers[c, y].color = EmptyCellColor;
                }
            }
            return clearRows.Count + clearCols.Count;
        }
    }
}
