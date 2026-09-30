using UnityEngine;
namespace BlockBlast.Utils {
    public static class SpriteFactory {
        private static Sprite _square, _circle;
        public static Sprite CreateSquare() {
            if (_square != null) return _square;
            var tex = new Texture2D(64, 64, TextureFormat.RGBA32, false);
            var px = new Color32[64 * 64];
            var w = new Color32(255, 255, 255, 255);
            for (int i = 0; i < px.Length; i++) px[i] = w;
            tex.SetPixels32(px); tex.Apply();
            return _square = Sprite.Create(tex, new Rect(0,0,64,64), new Vector2(0.5f,0.5f), 64);
        }
        public static Sprite CreateCircle() {
            if (_circle != null) return _circle;
            var tex = new Texture2D(64, 64, TextureFormat.RGBA32, false);
            var px = new Color32[64 * 64];
            var w = new Color32(255,255,255,255); var c = new Color32(255,255,255,0);
            for (int y=0; y<64; y++) for(int x=0; x<64; x++) px[y*64+x] = Vector2.Distance(new Vector2(x,y), new Vector2(32,32)) <= 31.5f ? w : c;
            tex.SetPixels32(px); tex.Apply();
            return _circle = Sprite.Create(tex, new Rect(0,0,64,64), new Vector2(0.5f,0.5f), 64);
        }
    }
}
