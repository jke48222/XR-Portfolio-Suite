using UnityEngine;

namespace ElementalBendingSandbox.Elements.Water
{
    /// <summary>
    /// Represents an environmental water supply. Volume is abstract; we only track normalized availability.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class WaterSource : MonoBehaviour
    {
        [SerializeField, Tooltip("Maximum normalized volume (0-1).")]
        private float _capacity = 1f;
        [SerializeField, Tooltip("Normalized units regenerated per second.")]
        private float _regenPerSecond = 0.15f;
        [SerializeField, Tooltip("Normalized draw rate per second at full gesture intensity.")]
        private float _maxDrawRate = 0.35f;
        [SerializeField, Tooltip("Meters. Hands must be within this radius to pull water.")]
        private float _drawRadius = 1.5f;

        public float Available01 => _currentVolume;

        private float _currentVolume;
        private Collider _collider;

        private void Awake()
        {
            _currentVolume = _capacity;
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        private void Update()
        {
            _currentVolume = Mathf.Clamp01(_currentVolume + _regenPerSecond * Time.deltaTime);
        }

        public bool IsInRange(Vector3 position)
        {
            var closest = _collider.ClosestPoint(position);
            return Vector3.SqrMagnitude(position - closest) <= _drawRadius * _drawRadius;
        }

        /// <summary>
        /// Attempts to draw normalized volume based on gesture intensity and dt.
        /// Returns actual drawn amount (0-1 normalized).
        /// </summary>
        public float Draw(float gestureIntensity01, float deltaTime)
        {
            if (_currentVolume <= 0f)
            {
                return 0f;
            }

            var desired = Mathf.Clamp01(gestureIntensity01) * _maxDrawRate * deltaTime;
            var pulled = Mathf.Min(desired, _currentVolume);
            _currentVolume -= pulled;
            return pulled;
        }
    }
}
