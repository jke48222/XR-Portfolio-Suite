using UnityEngine;

namespace ElementalBendingSandbox.Gestures
{
    /// <summary>
    /// Continuous gesture interpretation result. Provides smoothed physical intent
    /// rather than raw controller input to keep downstream systems deterministic.
    /// </summary>
    public struct GestureIntentData
    {
        public readonly Vector3 PrimaryDirection;    // Normalized world-space direction the player is pushing toward.
        public readonly float Intensity01;           // 0-1 intent strength after smoothing and thresholds.
        public readonly float Stability01;           // 0-1 stability estimate based on hand coherence.
        public readonly float Confidence01;          // 0-1 certainty of the gesture classification.
        public readonly Vector3 AverageHandPosition; // World-space average of both hands for posture-aware systems.
        public readonly float HandSeparation;        // Raw hand separation distance in meters.

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
