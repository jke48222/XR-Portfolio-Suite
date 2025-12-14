using UnityEngine;
using ElementalBendingSandbox.Gestures;

namespace ElementalBendingSandbox.Elements.Common
{
    /// <summary>
    /// Central router that feeds gesture intent into the currently active element controller.
    /// Rig-agnostic: expects external systems (Meta XR rig) to provide gesture data, and physical activators to set active element.
    /// </summary>
    public class ElementLoopCoordinator : MonoBehaviour
    {
        [SerializeField] private GestureEvaluator _gestureEvaluator;
        [SerializeField] private MonoBehaviour _airController;
        [SerializeField] private MonoBehaviour _waterController;
        [SerializeField] private MonoBehaviour _earthController;
        [SerializeField] private MonoBehaviour _fireController;
        [SerializeField] private ElementType _activeElement = ElementType.Air;

        public ElementType ActiveElement => _activeElement;

        private IElementController _air;
        private IElementController _water;
        private IElementController _earth;
        private IElementController _fire;

        private void Awake()
        {
            _air = _airController as IElementController;
            _water = _waterController as IElementController;
            _earth = _earthController as IElementController;
            _fire = _fireController as IElementController;
        }

        private void Reset()
        {
            _gestureEvaluator = GetComponentInChildren<GestureEvaluator>();
        }

        public void SetActiveElement(ElementType elementType)
        {
            _activeElement = elementType;
        }

        private void FixedUpdate()
        {
            if (_gestureEvaluator == null)
            {
                return;
            }

            var intent = _gestureEvaluator.CurrentIntent;
            var dt = Time.fixedDeltaTime;
            var controller = GetController(_activeElement);
            controller?.ProcessIntent(intent, dt);
        }

        private IElementController GetController(ElementType type)
        {
            return type switch
            {
                ElementType.Air => _air,
                ElementType.Water => _water,
                ElementType.Earth => _earth,
                ElementType.Fire => _fire,
                _ => null
            };
        }
    }
}
