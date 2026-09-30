#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BlockBlast.Editor {
    public static class AndroidBuilder {
        private const string ScenePath = "Assets/Scenes/Main.unity";
        private const string OutputPath = "Builds/Android/BlockBlast.apk";

        [MenuItem("BlockBlast/Build Android APK")]
        public static void Build() {
            if (!File.Exists(ScenePath)) SceneBuilder.BuildMainScene();

            AndroidBuildSettings.Configure();
            Directory.CreateDirectory(Path.GetDirectoryName(OutputPath));

            var options = new BuildPlayerOptions {
                scenes = new[] { ScenePath },
                target = BuildTarget.Android,
                locationPathName = OutputPath,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(options);
            Debug.Log("Android APK build result: " + report.summary.result);
            EditorApplication.Exit(report.summary.result == BuildResult.Succeeded ? 0 : 1);
        }
    }
}
#endif
