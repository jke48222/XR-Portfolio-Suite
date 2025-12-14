using XrShared.Core.Performance;
using XrShared.Core.Diagnostics;

namespace XrShared.Core.Capture
{
    public sealed class CaptureController
    {
        private readonly CaptureConfig _config;
        public bool IsCaptureMode { get; private set; }

        public CaptureController(CaptureConfig config)
        {
            _config = config;
        }

        public void SetCaptureMode(bool enable, PerformanceGovernor perf, DebugStats stats = null)
        {
            IsCaptureMode = enable;

            if (_config != null && _config.enableHighTierInCapture && perf != null)
            {
                perf.SetTier(enable ? PerformanceConfig.QualityTier.HighCapture : PerformanceConfig.QualityTier.Default, stats);
            }
        }
    }
}
