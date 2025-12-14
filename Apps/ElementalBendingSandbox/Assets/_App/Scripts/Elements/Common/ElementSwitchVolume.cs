using UnityEngine;
using ElementalBendingSandbox.Input;

namespace ElementalBendingSandbox.Elements.Common
{
    /// <summary>
    /// Physical activator that switches the active element when hands dwell inside the collider volume.
    /// Embodied switching keeps UX diegetic and rig-agnostic; swap Meta XR rig hand positions into the sampler when available.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ElementSwitchVolume : MonoBehaviour
    {
        [SerializeField] private ElementType _elementType;
        [SerializeField, Tooltip("Seconds hands must dwell in volume before switching.")]
        private float _dwellSeconds = 0.25f;
        [SerializeField] private ElementLoopCoordinator _coordinator;
        [SerializeField] private HandKinematicsSampler _handSampler;

        private Collider _collider;
        private float _dwellTimer;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        private void Reset()
        {
            _coordinator = FindObjectOfType<ElementLoopCoordinator>();
            _handSampler = FindObjectOfType<HandKinematicsSampler>();
        }

        private void Update()
        {
            if (_coordinator == null || _handSampler == null)
            {
                return;
            }

            var leftInside = _collider.bounds.Contains(_handSampler.LeftHand.Position);
            var rightInside = _collider.bounds.Contains(_handSampler.RightHand.Position);

            if (leftInside || rightInside)
            {
                _dwellTimer += Time.deltaTime;
                if (_dwellTimer >= _dwellSeconds)
                {
                    _coordinator.SetActiveElement(_elementType);
                }
            }
            else
            {
                _dwellTimer = 0f;
            }
        }
    }
}
