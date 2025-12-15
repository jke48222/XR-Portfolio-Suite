using UnityEngine;
using ElementalBendingSandbox.Elements.Common;

namespace ElementalBendingSandbox.Elements.Water
{
    /// <summary>
    /// Drives a cohesive water body using rigidbody forces. Approximates fluid by clamping speed and applying spring-like cohesion.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class WaterCohesiveVolume : MonoBehaviour
    {
        [SerializeField] private ElementDynamicsProfile _dynamics = new ElementDynamicsProfile { MaxAcceleration = 10f, MaxSpeed = 6f, ResponseTime = 0.16f };
        [SerializeField, Tooltip("Strength of cohesion force keeping the volume together.")]
        private float _cohesionStrength = 18f;
        [SerializeField, Tooltip("Viscosity-like drag applied every fixed frame.")]
        private float _viscosity = 2.2f;
        [SerializeField, Tooltip("How far the volume can stretch along the control direction (meters).")]
        private float _stretchDistance = 0.6f;
        [SerializeField, Tooltip("Angular damping to keep rotations calm.")]
        private float _angularDamping = 6f;

        private Rigidbody _rb;
        private Vector3 _smoothedDirection = Vector3.forward;
        private Vector3 _cohesionAnchor;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
            _cohesionAnchor = transform.position;
        }

        public void Drive(Vector3 direction, float intensity01, float availableVolume01, float stability01, float deltaTime)
        {
            _smoothedDirection = Vector3.Slerp(_smoothedDirection, direction, _dynamics.GetLerpFactor(deltaTime));
            _cohesionAnchor = Vector3.Lerp(_cohesionAnchor, transform.position + _smoothedDirection * _stretchDistance * intensity01, _dynamics.GetLerpFactor(deltaTime));

            var clampedIntensity = Mathf.Clamp01(intensity01);
            var accel = _smoothedDirection * (_dynamics.MaxAcceleration * clampedIntensity);
            _rb.AddForce(accel, ForceMode.Acceleration);

            // Cohesion keeps the body together and prevents runaway stretching while moving.
            var anchorForce = (_cohesionAnchor - transform.position) * _cohesionStrength * Mathf.Max(availableVolume01, 0.25f);
            _rb.AddForce(anchorForce, ForceMode.Acceleration);

            // Viscosity damps jitter while allowing responsiveness. Increased when stability drops.
            var damp = _viscosity * Mathf.Lerp(1f, 1.5f, 1f - stability01);
            _rb.AddForce(-_rb.linearVelocity * damp, ForceMode.Acceleration);
            _rb.linearVelocity = _dynamics.ClampVelocity(_rb.linearVelocity) * Mathf.Max(availableVolume01, 0.35f);
            _rb.angularVelocity = Vector3.Lerp(_rb.angularVelocity, Vector3.zero, _angularDamping * deltaTime);
        }
    }
}
