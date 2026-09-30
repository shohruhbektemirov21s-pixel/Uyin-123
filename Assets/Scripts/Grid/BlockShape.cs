using UnityEngine;
namespace BlockBlast.Grid {
    public struct BlockShape {
        public readonly bool[,] Matrix;
        public readonly int Width, Height;
        public readonly Color Color;
        public BlockShape(bool[,] matrix, Color color) {
            Matrix = matrix; Width = matrix.GetLength(0); Height = matrix.GetLength(1); Color = color;
        }
    }
}
