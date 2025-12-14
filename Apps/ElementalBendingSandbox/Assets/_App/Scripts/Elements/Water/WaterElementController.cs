using System;
using UnityEngine;
using ElementalBendingSandbox.Elements.Common;
using ElementalBendingSandbox.Gestures;

namespace ElementalBendingSandbox.Elements.Water
{
    /// <summary>
    /// State-driven controller for water manipulation. Draws volume from nearby sources and pushes a cohesive body with gradual forces.
    /// </summary>
    public class WaterElementController : MonoBehaviour, IElementController
    {
        [Header("References")]
        [SerializeField] private WaterCohesiveVolume _waterBody;
        [SerializeField] private WaterSource _source;

        [Header("Tuning")]
        [SerializeField, Tooltip("Intent required to start pulling water.")]
        private float _engageThreshold = 0.15f;
        [SerializeField, Tooltip("Intent below this stops active flow.")]
        private float _releaseThreshold = 0.08f;
        [SerializeField, Tooltip("Rate at which internal volume fills when drawing from a source.")]
        private float _fillRate = 1.2f;
        [SerializeField, Tooltip("Rate at which volume drains when no source is present.")]
        private float _decayRate = 0.5f;
        [SerializeField, Tooltip("Minimum stability required to avoid splashing/instability.")]
        private float _stabilityRequirement = 0.25f;

        public ElementState CurrentState => _currentState;
        public ElementOutputState Output => _output;
        public float CurrentVolume01 => _currentVolume;

        private ElementState _currentState = ElementState.Idle;
        private ElementOutputState _output;
        private float _currentVolume;

        public event Action<ElementOutputState> OnWaterOutputChanged;

        private void Awake()
        {
            _currentVolume = 0f;
            _output = new ElementOutputState(_currentState, Vector3.forward, 0f, 0f, 0f, 1f);
        }

        public void ProcessIntent(in GestureIntentData intent, float deltaTime)
        {
            var sourceInRange = _source != null && _source.IsInRange(intent.AverageHandPosition);
            if (sourceInRange)
            {
                var pulled = _source.Draw(intent.Intensity01, deltaTime) * _fillRate;
                _currentVolume = Mathf.Clamp01(_currentVolume + pulled);
            }
            else
            {
                _currentVolume = Mathf.Clamp01(_currentVolume - _decayRate * deltaTime);
            }

            var instability = 1f - Mathf.Clamp01(intent.Stability01 * intent.Confidence01);
            var usableIntensity = Mathf.Clamp01(intent.Intensity01 * _currentVolume);
            if (instability > (1f - _stabilityRequirement))
            {
                usableIntensity *= 0.5f; // damp when input is noisy
            }

            UpdateState(usableIntensity, sourceInRange);

            if (_currentState != ElementState.Idle && _waterBody != null)
            {
                _waterBody.Drive(intent.PrimaryDirection, usableIntensity, _currentVolume, 1f - instability, deltaTime);
            }

            _output = new ElementOutputState(_currentState, intent.PrimaryDirection, usableIntensity, intent.Stability01, intent.Confidence01, instability);
            OnWaterOutputChanged?.Invoke(_output);
        }

        private void UpdateState(float usableIntensity, bool sourceInRange)
        {
            if (!sourceInRange || _currentVolume <= 0.01f)
            {
                _currentState = ElementState.Idle;
                return;
            }

            if (usableIntensity <= _releaseThreshold)
            {
                _currentState = ElementState.Releasing;
                return;
            }

            if (_currentState == ElementState.Idle && usableIntensity > _engageThreshold)
            {
                _currentState = ElementState.Engaging;
                return;
            }

            if ((_currentState == ElementState.Engaging || _currentState == ElementState.Releasing) && usableIntensity > _releaseThreshold)
            {
                _currentState = ElementState.Sustaining;
                return;
            }

            if (_currentState == ElementState.Sustaining && usableIntensity < _releaseThreshold)
            {
                _currentState = ElementState.Releasing;
            }
        }
    }
}
