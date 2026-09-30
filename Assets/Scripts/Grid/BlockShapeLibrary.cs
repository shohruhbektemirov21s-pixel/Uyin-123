using UnityEngine;
namespace BlockBlast.Grid {
    public static class BlockShapeLibrary {
        public static BlockShape[] Shapes;
        static BlockShapeLibrary() {
            Shapes = new BlockShape[] {
                new BlockShape(new bool[,] {{true}}, Color.red),
                new BlockShape(new bool[,] {{true, true}}, Color.blue),
                new BlockShape(new bool[,] {{true}, {true}}, Color.blue),
                new BlockShape(new bool[,] {{true, true, true}}, Color.green),
                new BlockShape(new bool[,] {{true}, {true}, {true}}, Color.green),
                new BlockShape(new bool[,] {{true, true}, {true, true}}, Color.yellow),
                new BlockShape(new bool[,] {{true, true, true}, {true, false, false}, {true, false, false}}, Color.magenta)
            };
        }
    }
}
