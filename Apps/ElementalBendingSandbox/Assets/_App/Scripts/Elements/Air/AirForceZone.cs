using UnityEngine;
using ElementalBendingSandbox.Elements.Common;

namespace ElementalBendingSandbox.Elements.Air
{
    /// <summary>
    /// Applies directional force to rigidbodies within the zone based on air element output.
    /// Uses trigger volume to avoid per-frame allocations and keeps forces clamped for stability on Quest.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class AirForceZone : MonoBehaviour
    {
        [SerializeField] private AirElementController _controller;
        [SerializeField, Tooltip("Meters per second squared at full intent.")]
        private float _maxAcceleration = 18f;
        [SerializeField, Tooltip("Force mode used for airflow.")]
        private ForceMode _forceMode = ForceMode.Acceleration;
        [SerializeField, Tooltip("Maximum rigidbody mass affected (heavier objects ignore airflow).")]
        private float _massCutoff = 50f;
        [SerializeField, Tooltip("Optional falloff for objects near the edge of the zone.")]
        private AnimationCurve _edgeFalloff = AnimationCurve.Linear(0f, 1f, 1f, 0.65f);

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_controller == null || _controller.CurrentState == ElementState.Idle)
            {
                return;
            }

            var rb = other.attachedRigidbody;
            if (rb == null || rb.mass > _massCutoff)
            {
                return;
            }

            var intensity = Mathf.Clamp01(_controller.CurrentIntensity01);
            if (intensity <= 0.001f)
            {
                return;
            }

            var direction = _controller.CurrentDirection;
            var center = _collider.bounds.center;
            var normalizedDistance = Mathf.Clamp01(Vector3.Distance(center, other.ClosestPoint(center)) / (_collider.bounds.extents.magnitude + 0.0001f));
            var falloff = _edgeFalloff.Evaluate(normalizedDistance);

            var accel = direction * (_maxAcceleration * intensity * falloff);
            // Acceleration mode keeps behavior mass-independent; consistent feel for throwable props.
            rb.AddForce(accel, _forceMode);
        }
    }
}
