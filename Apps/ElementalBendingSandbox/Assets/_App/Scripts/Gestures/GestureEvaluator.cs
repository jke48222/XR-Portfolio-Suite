using UnityEngine;
using UnityEngine.XR;
using ElementalBendingSandbox.Input;

namespace ElementalBendingSandbox.Gestures
{
    /// <summary>
    /// Converts raw hand kinematics into continuous gesture intent suitable for element controllers.
    /// The current heuristic assumes "push air" gesture: both hands moving in similar direction with some spacing.
    /// No spell names are hard-coded; thresholds are tunable for future elements.
    /// </summary>
    public class GestureEvaluator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private HandKinematicsSampler _handSampler;

        [Header("Tuning")]
        [SerializeField, Tooltip("Minimum combined hand speed (m/s) to consider as intentional gesture.")]
        private float _minActivationSpeed = 0.2f;
        [SerializeField, Tooltip("Speed at which intent ramps to full (m/s)."), Min(0.1f)]
        private float _fullIntentSpeed = 1.4f;
        [SerializeField, Tooltip("Distance range that yields stable intent (meters).")]
        private Vector2 _handSeparationRange = new Vector2(0.15f, 0.6f);
        [SerializeField, Tooltip("How quickly intent responds to input changes (seconds).")]
        private float _intentSmoothing = 0.08f;

        public GestureIntentData CurrentIntent => _currentIntent;

        private GestureIntentData _currentIntent = new GestureIntentData(Vector3.forward, 0f, 0f, 0f);

        private void Reset()
        {
            _handSampler = GetComponentInChildren<HandKinematicsSampler>();
        }

        private void FixedUpdate()
        {
            if (_handSampler == null)
            {
                return;
            }

            var left = _handSampler.LeftHand;
            var right = _handSampler.RightHand;

            var coherence = ComputeDirectionalCoherence(left.SmoothedVelocity, right.SmoothedVelocity);
            var averageDirection = ComputeAverageDirection(left.SmoothedVelocity, right.SmoothedVelocity);
            var combinedSpeed = left.SmoothedVelocity.magnitude + right.SmoothedVelocity.magnitude;
            var handSeparation = Vector3.Distance(left.Position, right.Position);

            var stability = ComputeStability(coherence, handSeparation);
            var rawIntent = Mathf.InverseLerp(_minActivationSpeed, _fullIntentSpeed, combinedSpeed);
            var intensity = Mathf.Clamp01(Mathf.Lerp(_currentIntent.Intensity01, rawIntent, FixedDeltaRatio(_intentSmoothing)));
            var confidence = Mathf.Clamp01(coherence * stability);

            _currentIntent = new GestureIntentData(
                averageDirection,
                intensity,
                stability,
                confidence
            );
        }

        private static float ComputeDirectionalCoherence(Vector3 leftVel, Vector3 rightVel)
        {
            if (leftVel == Vector3.zero || rightVel == Vector3.zero)
            {
                return 0f;
            }

            var dot = Vector3.Dot(leftVel.normalized, rightVel.normalized);
            // Dot in [-1,1] -> clamp to [0,1] to treat opposition as instability.
            return Mathf.Clamp01((dot + 1f) * 0.5f);
        }

        private float ComputeStability(float coherence, float separation)
        {
            var separationScore = Mathf.InverseLerp(_handSeparationRange.x, _handSeparationRange.y, separation);
            // Penalize if outside desired separation.
            if (separation < _handSeparationRange.x || separation > _handSeparationRange.y)
            {
                separationScore *= 0.35f;
            }

            return Mathf.Clamp01(0.5f * coherence + 0.5f * separationScore);
        }

        private static Vector3 ComputeAverageDirection(Vector3 leftVel, Vector3 rightVel)
        {
            var sum = leftVel + rightVel;
            if (sum.sqrMagnitude < 0.0001f)
            {
                return Vector3.forward; // Fallback to stable forward reference.
            }
            return sum.normalized;
        }

        private static float FixedDeltaRatio(float smoothingSeconds)
        {
            // Convert smoothing time to lerp coefficient. Keeps smoothing consistent if fixedDeltaTime changes.
            var dt = Time.fixedDeltaTime;
            return Mathf.Clamp01(dt / Mathf.Max(smoothingSeconds, 0.0001f));
        }
    }
}
