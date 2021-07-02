using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class PostBuildProcess : MonoBehaviour {
    [PostProcessBuild]
    public static void OnPostProcessBuild (BuildTarget buildTarget, string path) {
        if (buildTarget == BuildTarget.iOS) {
            string pjPath = PBXProject.GetPBXProjectPath (path);
            PBXProject pj = new PBXProject ();
            pj.ReadFromString (File.ReadAllText (pjPath));

#if UNITY_2019_3_OR_NEWER
            string targetName = pj.GetUnityMainTargetGuid();
#else
            string targetName = pj.TargetGuidByName("Unity-iPhone");
#endif
            pj.AddBuildProperty (targetName, "OTHER_LDFLAGS", "-ObjC");
            pj.AddBuildProperty (targetName, "CLANG_ALLOW_NON_MODULAR_INCLUDES_IN_FRAMEWORK_MODULES", "YES");
            pj.AddBuildProperty (targetName, "ENABLE_BITCODE", "NO");

#if UNITY_2019_3_OR_NEWER
            string unityFrameWorkTargetName = pj.GetUnityFrameworkTargetGuid();
#else
            string unityFrameWorkTargetName = pj.TargetGuidByName(PBXProject.GetUnityTargetName());
#endif
            pj.AddBuildProperty (unityFrameWorkTargetName, "OTHER_CFLAGS", "-fmodules");
            pj.AddBuildProperty (unityFrameWorkTargetName, "OTHER_CFLAGS", "-fcxx-modules");
            pj.AddBuildProperty (unityFrameWorkTargetName, "ENABLE_BITCODE", "NO");

            File.WriteAllText (pjPath, pj.WriteToString ());
        }
    }
}