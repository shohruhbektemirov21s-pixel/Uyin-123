#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

namespace BlockBlast.Editor {
    public static class AppIconBuilder {
        [MenuItem("BlockBlast/Generate Placeholder Icon")]
        public static void GenerateIcon() {
            int size = 512;
            Texture2D tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
            Color32[] pixels = new Color32[size * size];
            Color32 bg = new Color32(30, 30, 30, 255);
            Color32 block = new Color32(255, 100, 100, 255);
            for (int y = 0; y < size; y++) {
                for (int x = 0; x < size; x++) {
                    bool isBlock = x > 100 && x < 412 && y > 100 && y < 412;
                    pixels[y * size + x] = isBlock ? block : bg;
                }
            }
            tex.SetPixels32(pixels);
            byte[] png = tex.EncodeToPNG();
            File.WriteAllBytes("Assets/AppIcon.png", png);
            AssetDatabase.Refresh();
            Debug.Log("Icon generated at Assets/AppIcon.png");
        }
    }
}
#endif
