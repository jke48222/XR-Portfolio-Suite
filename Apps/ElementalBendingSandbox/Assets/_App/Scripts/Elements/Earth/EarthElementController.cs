using UnityEngine;
using ElementalBendingSandbox.Elements.Common;
using ElementalBendingSandbox.Gestures;

namespace ElementalBendingSandbox.Elements.Earth
{
    /// <summary>
    /// Earth feels heavy and requires grounded posture plus commitment before movement ramps.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class EarthElementController : MonoBehaviour, IElementController
    {
        [Header("References")]
        [SerializeField, Tooltip("Head transform used for posture checks (Meta XR rig camera).")]
        private Transform _head;

        [Header("Dynamics")]
        [SerializeField] private ElementDynamicsProfile _dynamics = new ElementDynamicsProfile { MaxAcceleration = 6f, MaxSpeed = 4f, ResponseTime = 0.28f, InstabilityDamping = 0.55f };
        [SerializeField, Tooltip("Seconds of sustained intent before the chunk commits to moving.")]
        private float _commitSeconds = 0.35f;
        [SerializeField, Tooltip("Hands must be this far below head height to enable control (meters).")]
        private float _groundedOffset = 0.15f;
        [SerializeField, Tooltip("Minimal intent to start commitment.")]
        private float _engageThreshold = 0.18f;
        [SerializeField, Tooltip("Intent below this releases control.")]
        private float _releaseThreshold = 0.08f;

        public ElementState CurrentState => _currentState;
        public ElementOutputState Output => _output;

        private Rigidbody _rb;
        private ElementState _currentState = ElementState.Idle;
        private ElementOutputState _output;
        private float _commitTimer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
            _output = new ElementOutputState(_currentState, Vector3.forward, 0f, 0f, 0f, 1f);
        }

        public void ProcessIntent(in GestureIntentData intent, float deltaTime)
        {
            var grounded = IsGrounded(intent.AverageHandPosition);
            var instability = 1f - Mathf.Clamp01(intent.Stability01 * intent.Confidence01);
            var intensity = grounded ? intent.Intensity01 : 0f;

            if (intensity > _engageThreshold && grounded)
            {
                _commitTimer += deltaTime;
            }
            else
            {
                _commitTimer = Mathf.Max(0f, _commitTimer - deltaTime);
            }

            UpdateState(intensity, grounded);

            if (_currentState == ElementState.Sustaining || _currentState == ElementState.Engaging)
            {
                var planarDir = Vector3.ProjectOnPlane(intent.PrimaryDirection, Vector3.up).normalized;
                var response = Mathf.Clamp01(_commitTimer / _commitSeconds);
                var targetAccel = planarDir * (_dynamics.MaxAcceleration * intensity * response);
                _rb.AddForce(targetAccel, ForceMode.Acceleration);
                _rb.AddForce(-_rb.velocity * _dynamics.InstabilityDamping * instability, ForceMode.Acceleration);
                _rb.velocity = _dynamics.ClampVelocity(_rb.velocity * Mathf.Lerp(1f, 0.9f, instability));
            }
            else if (_currentState == ElementState.Releasing)
            {
                _rb.AddForce(-_rb.velocity * (_dynamics.InstabilityDamping + 0.5f) * deltaTime, ForceMode.VelocityChange);
            }

            _output = new ElementOutputState(_currentState, intent.PrimaryDirection, intensity, intent.Stability01, intent.Confidence01, instability);
        }

        private bool IsGrounded(Vector3 averageHandPosition)
        {
            if (_head == null && Camera.main != null)
            {
                _head = Camera.main.transform;
            }

            if (_head == null)
            {
                return true; // fall back to always enabled if head is not yet wired
            }

            return (_head.position.y - averageHandPosition.y) >= _groundedOffset;
        }

        private void UpdateState(float intensity, bool grounded)
        {
            if (!grounded || intensity < _releaseThreshold)
            {
                _currentState = ElementState.Idle;
                return;
            }

            if (_currentState == ElementState.Idle && intensity >= _engageThreshold)
            {
                _currentState = ElementState.Engaging;
                return;
            }

            if (_currentState == ElementState.Engaging && _commitTimer >= _commitSeconds)
            {
                _currentState = ElementState.Sustaining;
                return;
            }

            if (_currentState == ElementState.Sustaining && intensity < _releaseThreshold)
            {
                _currentState = ElementState.Releasing;
            }
        }
    }
}
