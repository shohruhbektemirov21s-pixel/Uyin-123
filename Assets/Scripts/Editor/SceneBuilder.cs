#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace BlockBlast.Editor {
    public static class SceneBuilder {
        [MenuItem("BlockBlast/Build Main Scene")]
        public static void BuildMainScene() {
            System.IO.Directory.CreateDirectory("Assets/Scenes");
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            
            var camGo = new GameObject("Main Camera");
            var cam = camGo.AddComponent<Camera>();
            cam.orthographic = true;
            cam.tag = "MainCamera";
            
            var bootGo = new GameObject("Bootstrapper");
            bootGo.AddComponent<BlockBlast.Bootstrap.Bootstrapper>();
            
            EditorSceneManager.SaveScene(scene, "Assets/Scenes/Main.unity");
            Debug.Log("Scene built successfully.");
        }
    }
}
#endif
