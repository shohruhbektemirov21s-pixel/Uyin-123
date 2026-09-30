#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace BlockBlast.Editor {
    public static class WebGLBuilder {
        public static void Build() {
            var scenePath = "Assets/Scenes/Main.unity";
            if (!System.IO.File.Exists(scenePath)) {
                SceneBuilder.BuildMainScene();
            }
            
            var options = new BuildPlayerOptions {
                scenes = new[] { scenePath },
                target = BuildTarget.WebGL,
                locationPathName = "D:/Uyin/WebGLBuild",
                options = BuildOptions.None
            };
            
            PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
            
            var report = BuildPipeline.BuildPlayer(options);
            Debug.Log("Build result: " + report.summary.result);
            if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded) {
                EditorApplication.Exit(1);
            } else {
                EditorApplication.Exit(0);
            }
        }
    }
}
#endif
