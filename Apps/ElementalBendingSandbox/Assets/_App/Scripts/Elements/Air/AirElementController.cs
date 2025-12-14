using System;
using UnityEngine;
using ElementalBendingSandbox.Elements.Common;
using ElementalBendingSandbox.Gestures;

namespace ElementalBendingSandbox.Elements.Air
{
    /// <summary>
    /// State-driven controller for air manipulation. Maps continuous gesture intent to a stable airflow output.
    /// </summary>
    public class AirElementController : MonoBehaviour, IElementController
    {
        [Header("Tuning")]
        [SerializeField, Tooltip("Intent required to start moving air.")]
        private float _engageThreshold = 0.12f;
        [SerializeField, Tooltip("Intent below this stops active pushing.")]
        private float _releaseThreshold = 0.06f;
        [SerializeField, Tooltip("Rate at which airflow ramps up/down (seconds).")]
        private float _intensitySmoothing = 0.12f;
        [SerializeField, Tooltip("Blend speed for airflow direction (seconds).")]
        private float _directionSmoothing = 0.08f;
        [SerializeField, Tooltip("Confidence required to avoid unstable state.")]
        private float _stabilityRequirement = 0.35f;

        [Header("Outputs (read-only)")]
        [SerializeField] private ElementState _currentState = ElementState.Idle;
        [SerializeField] private float _currentIntensity;
        [SerializeField] private Vector3 _currentDirection = Vector3.forward;
        [SerializeField, Tooltip("Higher = more jitter in input; useful for VFX hooks.")]
        private float _instability;

        public ElementState CurrentState => _currentState;
        public float CurrentIntensity01 => _currentIntensity;
        public Vector3 CurrentDirection => _currentDirection;
        public float Instability01 => _instability;

        public event Action<float, Vector3, float> OnAirIntentChanged; // intensity, direction, instability

        public void ProcessIntent(in GestureIntentData intent, float deltaTime)
        {
            var directionLerp = Mathf.Clamp01(deltaTime / Mathf.Max(_directionSmoothing, 0.0001f));
            _currentDirection = Vector3.Slerp(_currentDirection, intent.PrimaryDirection, directionLerp).normalized;

            var intensityTarget = intent.Intensity01;
            var intensityLerp = Mathf.Clamp01(deltaTime / Mathf.Max(_intensitySmoothing, 0.0001f));
            _currentIntensity = Mathf.Lerp(_currentIntensity, intensityTarget, intensityLerp);

            _instability = 1f - Mathf.Clamp01(intent.Stability01 * intent.Confidence01);

            UpdateState(intent);
            OnAirIntentChanged?.Invoke(_currentIntensity, _currentDirection, _instability);
        }

        private void UpdateState(in GestureIntentData intent)
        {
            if (intent.Confidence01 < _stabilityRequirement)
            {
                _currentState = ElementState.Unstable;
                _currentIntensity *= 0.5f; // damp runaway forces when input quality is low
                return;
            }

            if (_currentIntensity <= _releaseThreshold)
            {
                _currentState = ElementState.Idle;
                return;
            }

            if (_currentState == ElementState.Idle && _currentIntensity > _engageThreshold)
            {
                _currentState = ElementState.Engaging;
                return;
            }

            if (_currentState == ElementState.Engaging && _currentIntensity > _releaseThreshold)
            {
                _currentState = ElementState.Sustaining;
                return;
            }

            if (_currentState == ElementState.Sustaining && _currentIntensity < _releaseThreshold)
            {
                _currentState = ElementState.Releasing;
                return;
            }

            if (_currentState == ElementState.Releasing && _currentIntensity < _releaseThreshold * 0.5f)
            {
                _currentState = ElementState.Idle;
            }
        }
    }
}
