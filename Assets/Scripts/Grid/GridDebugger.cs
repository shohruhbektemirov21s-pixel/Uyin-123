using UnityEngine;
namespace BlockBlast.Grid {
    public class GridDebugger : MonoBehaviour {
        private void OnDrawGizmos() {
            Gizmos.color = Color.white;
            for (int x = 0; x <= 8; x++) Gizmos.DrawLine(new Vector3(x - 0.5f, -0.5f), new Vector3(x - 0.5f, 7.5f));
            for (int y = 0; y <= 8; y++) Gizmos.DrawLine(new Vector3(-0.5f, y - 0.5f), new Vector3(7.5f, y - 0.5f));
        }
    }
}
