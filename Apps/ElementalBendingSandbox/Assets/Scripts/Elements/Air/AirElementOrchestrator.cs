using UnityEngine;
using ElementalBendingSandbox.Gestures;

namespace ElementalBendingSandbox.Elements.Air
{
    /// <summary>
    /// Connects the gesture evaluator to the AirElementController. Keeps control loop deterministic and centralized.
    /// </summary>
    public class AirElementOrchestrator : MonoBehaviour
    {
        [SerializeField] private GestureEvaluator _gestureEvaluator;
        [SerializeField] private AirElementController _airController;

        private void Reset()
        {
            _gestureEvaluator = GetComponentInChildren<GestureEvaluator>();
            _airController = GetComponentInChildren<AirElementController>();
        }

        private void FixedUpdate()
        {
            if (_gestureEvaluator == null || _airController == null)
            {
                return;
            }

            var intent = _gestureEvaluator.CurrentIntent;
            _airController.ProcessIntent(intent, Time.fixedDeltaTime);
        }
    }
}
