using UnityEngine;

namespace XrShared.Core.Performance
{
    [CreateAssetMenu(menuName = "XR Shared/Performance Config", fileName = "PerformanceConfig")]
    public class PerformanceConfig : ScriptableObject
    {
        public enum QualityTier
        {
            Low,
            Default,
            HighCapture
        }

        [Header("Caps")]
        public int maxParticleSystems = 12;
        public int maxActiveRigidbodies = 80;
        public int maxAudioSources = 24;

        [Header("Defaults")]
        public QualityTier defaultTier = QualityTier.Default;
    }
}
