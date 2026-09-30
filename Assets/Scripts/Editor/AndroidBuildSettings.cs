#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace BlockBlast.Editor {
    public static class AndroidBuildSettings {
        [MenuItem("BlockBlast/Configure Android Settings")]
        public static void Configure() {
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
            PlayerSettings.allowedAutorotateToLandscapeLeft = false;
            PlayerSettings.allowedAutorotateToLandscapeRight = false;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel26; // Android 8.0
            EditorUserBuildSettings.buildAppBundle = false;
            PlayerSettings.productName = "Block Blast";
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.blockblast.game");
            Debug.Log("Android settings configured for APK release.");
        }
    }
}
#endif
