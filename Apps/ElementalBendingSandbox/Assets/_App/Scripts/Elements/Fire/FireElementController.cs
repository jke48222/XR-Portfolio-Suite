using UnityEngine;
using ElementalBendingSandbox.Elements.Common;
using ElementalBendingSandbox.Gestures;

namespace ElementalBendingSandbox.Elements.Fire
{
    /// <summary>
    /// Fire is reactive and instability-prone. Emits directional impulses with dispersion when over-driven.
    /// </summary>
    public class FireElementController : MonoBehaviour, IElementController
    {
        [Header("References")]
        [SerializeField] private FireEmissionVolume _emissionVolume;

        [Header("Dynamics")]
        [SerializeField] private ElementDynamicsProfile _dynamics = new ElementDynamicsProfile { MaxAcceleration = 25f, MaxSpeed = 18f, ResponseTime = 0.06f, InstabilityDamping = 0.2f };
        [SerializeField, Tooltip("Intent needed before emission starts.")]
        private float _igniteThreshold = 0.08f;
        [SerializeField, Tooltip("Intent below this stops emission.")]
        private float _extinguishThreshold = 0.04f;
        [SerializeField, Tooltip("Multiplier for dispersion based on instability.")]
        private float _spreadPerInstability = 9f;

        public ElementState CurrentState => _currentState;
        public ElementOutputState Output => _output;

        private ElementState _currentState = ElementState.Idle;
        private ElementOutputState _output;
        private float _currentIntensity;

        private void Awake()
        {
            _output = new ElementOutputState(_currentState, Vector3.forward, 0f, 0f, 0f, 1f);
        }

        public void ProcessIntent(in GestureIntentData intent, float deltaTime)
        {
            var instability = 1f - Mathf.Clamp01(intent.Stability01 * intent.Confidence01);
            var lerp = _dynamics.GetLerpFactor(deltaTime);
            var targetIntensity = Mathf.Clamp01(intent.Intensity01);
            _currentIntensity = Mathf.Lerp(_currentIntensity, targetIntensity, lerp);

            UpdateState(_currentIntensity);

            if (_currentState != ElementState.Idle && _emissionVolume != null)
            {
                var spread = _spreadPerInstability * instability;
                _emissionVolume.Emit(intent.PrimaryDirection, _currentIntensity, instability, spread, _dynamics);
            }

            _output = new ElementOutputState(_currentState, intent.PrimaryDirection, _currentIntensity, intent.Stability01, intent.Confidence01, instability);
        }

        private void UpdateState(float intensity)
        {
            if (intensity < _extinguishThreshold)
            {
                _currentState = ElementState.Idle;
                return;
            }

            if (_currentState == ElementState.Idle && intensity >= _igniteThreshold)
            {
                _currentState = ElementState.Engaging;
                return;
            }

            if (_currentState == ElementState.Engaging && intensity >= _igniteThreshold * 1.5f)
            {
                _currentState = ElementState.Sustaining;
                return;
            }

            if (_currentState == ElementState.Sustaining && intensity < _igniteThreshold)
            {
                _currentState = ElementState.Releasing;
                return;
            }

            if (_currentState == ElementState.Releasing && intensity < _extinguishThreshold)
            {
                _currentState = ElementState.Idle;
            }
        }
    }
}
