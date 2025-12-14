using UnityEngine;

namespace MRPocketPortals
{
    /// <summary>
    /// App-specific service installer for MR Pocket Portals.
    /// Portal placement, universe loading, anchors, passthrough,
    /// and occlusion services will be registered here later.
    /// </summary>
    public static class PocketPortalsInstaller
    {
        public static void Install()
        {
            // Intentionally empty.
            // This will later register:
            // - MetaAnchorStore
            // - MetaPassthroughController
            // - MetaOcclusionController
            // - Portal and universe systems
        }
    }
}
