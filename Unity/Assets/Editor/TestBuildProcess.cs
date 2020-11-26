using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class TestBuildProcess : IPreprocessBuildWithReport, IPostprocessBuildWithReport {
#if UNITY_IOS || UNITY_ANDROID
    string originalPath = Application.streamingAssetsPath + "/karte_tracker_config.json";
    string tmpPath = Application.streamingAssetsPath + "/karte_tracker_config.json.bak";
#endif

    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild (BuildReport report) {
        replaceConfigFile (report);
    }

    public void OnPostprocessBuild (BuildReport report) {
        restoreConfigFile (report);
    }

    private void replaceConfigFile (BuildReport report) {
#if UNITY_IOS || UNITY_ANDROID
        if ((report.summary.options & BuildOptions.IncludeTestAssemblies) > 0) {
            string contents = @"{
  ""appKey"": ""appkey"",
  ""trackerConfig"": {
      ""endpointUrl"": ""http://127.0.0.1:8010/v0/native""
  }
}";
            if (File.Exists (tmpPath)) {
                File.Delete (tmpPath);
            }
            File.Move (originalPath, tmpPath);
            var sr = File.CreateText (originalPath);
            sr.Write (contents);
            sr.Close ();
        }
#endif
    }

    private void restoreConfigFile (BuildReport report) {
#if UNITY_IOS || UNITY_ANDROID
        if ((report.summary.options & BuildOptions.IncludeTestAssemblies) > 0) {
            Debug.Log ("restoring");
            if (File.Exists (tmpPath) && File.Exists (originalPath)) {
                File.Delete (originalPath);
                File.Move (tmpPath, originalPath);
            }
        }
#endif
    }
}