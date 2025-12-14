using System;

namespace XrShared.Core.Diagnostics
{
    public sealed class DebugStats
    {
        public int activeParticles;
        public int activeRigidbodies;
        public int activeAudioSources;

        public long managedAllocBytesPerFrame;
        public float fps;

        public string qualityTier = "Default";

        public void ResetFrameStats()
        {
            activeParticles = 0;
            activeRigidbodies = 0;
            activeAudioSources = 0;
            managedAllocBytesPerFrame = 0;
        }

        public override string ToString()
        {
            return $"FPS {fps:0} | Alloc {managedAllocBytesPerFrame} B/f | Part {activeParticles} | RB {activeRigidbodies} | Aud {activeAudioSources} | Q {qualityTier}";
        }
    }
}
