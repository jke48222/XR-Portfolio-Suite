using UnityEngine;

namespace XrShared.Core.App
{
    [CreateAssetMenu(menuName = "XR Shared/App Config", fileName = "AppConfig")]
    public class AppConfig : ScriptableObject
    {
        [Header("Identity")]
        public string appName = "XR App";
        public string appVersion = "0.1.0";

        [Header("Scenes")]
        [Tooltip("Optional: scene name to load additively after bootstrap is initialized.")]
        public string experienceSceneName = "";

        [Header("Persistence")]
        public string saveFileName = "save.json";
        public int saveSchemaVersion = 1;

        [Header("Performance")]
        public Performance.PerformanceConfig performanceConfig;

        [Header("Capture")]
        public Capture.CaptureConfig captureConfig;

        [Header("Diagnostics")]
        public bool enableDebugOverlayInDev = true;
    }
}
