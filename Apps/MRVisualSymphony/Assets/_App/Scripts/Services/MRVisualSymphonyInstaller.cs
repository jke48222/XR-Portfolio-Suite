using UnityEngine;

namespace MRVisualSymphony
{
    /// <summary>
    /// App-specific service installer for MR Visual Symphony.
    /// Meta XR anchors, passthrough, occlusion, and audio routing
    /// will be registered here later.
    /// </summary>
    public static class MRVisualSymphonyInstaller
    {
        public static void Install()
        {
            // Intentionally empty.
            // This will later register:
            // - MetaAnchorStore
            // - MetaPassthroughController
            // - MetaOcclusionController
            // - MR-specific audio routing services
        }
    }
}
