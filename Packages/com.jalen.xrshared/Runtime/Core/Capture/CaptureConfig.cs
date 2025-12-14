using UnityEngine;

namespace XrShared.Core.Capture
{
    [CreateAssetMenu(menuName = "XR Shared/Capture Config", fileName = "CaptureConfig")]
    public class CaptureConfig : ScriptableObject
    {
        public bool enableHighTierInCapture = true;
        public bool hideUiInCapture = true;
        public bool disableOverlayInCapture = true;
    }
}
