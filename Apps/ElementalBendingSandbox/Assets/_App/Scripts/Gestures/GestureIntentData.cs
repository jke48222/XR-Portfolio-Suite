using UnityEngine;

namespace ElementalBendingSandbox.Gestures
{
    /// <summary>
    /// Continuous gesture interpretation result. Provides smoothed physical intent
    /// rather than raw controller input to keep downstream systems deterministic.
    /// </summary>
    public struct GestureIntentData
    {
        public Vector3 PrimaryDirection { get; }    // Normalized world-space direction the player is pushing toward.
        public float Intensity01 { get; }           // 0-1 intent strength after smoothing and thresholds.
        public float Stability01 { get; }           // 0-1 stability estimate based on hand coherence.
        public float Confidence01 { get; }          // 0-1 certainty of the gesture classification.
        public Vector3 AverageHandPosition { get; } // World-space average of both hands for posture-aware systems.
        public float HandSeparation { get; }        // Raw hand separation distance in meters.

        public GestureIntentData(Vector3 direction, float intensity01, float stability01, float confidence01, Vector3 averageHandPosition, float handSeparation)
        {
            PrimaryDirection = direction;
            Intensity01 = intensity01;
            Stability01 = stability01;
            Confidence01 = confidence01;
            AverageHandPosition = averageHandPosition;
            HandSeparation = handSeparation;
        }
    }
}
