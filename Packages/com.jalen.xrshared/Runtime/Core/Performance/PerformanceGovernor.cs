using XrShared.Core.Diagnostics;

namespace XrShared.Core.Performance
{
    public sealed class PerformanceGovernor
    {
        private readonly PerformanceConfig _config;
        public PerformanceConfig.QualityTier Tier { get; private set; }

        public PerformanceGovernor(PerformanceConfig config)
        {
            _config = config;
            Tier = config != null ? config.defaultTier : PerformanceConfig.QualityTier.Default;
        }

        public void SetTier(PerformanceConfig.QualityTier tier, DebugStats stats = null)
        {
            Tier = tier;
            if (stats != null) stats.qualityTier = tier.ToString();
        }

        public int MaxParticles => _config != null ? _config.maxParticleSystems : 12;
        public int MaxRigidbodies => _config != null ? _config.maxActiveRigidbodies : 80;
        public int MaxAudioSources => _config != null ? _config.maxAudioSources : 24;
    }
}
