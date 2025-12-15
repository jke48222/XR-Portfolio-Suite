using UnityEngine;
using ElementalBendingSandbox.Elements.Common;

namespace ElementalBendingSandbox.Elements.Fire
{
    /// <summary>
    /// Emits directional forces to nearby rigidbodies to simulate reactive fire jets. Uses non-alloc queries for Quest performance.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class FireEmissionVolume : MonoBehaviour
    {
        [SerializeField, Tooltip("Meters radius for overlap checks.")]
        private float _radius = 1.2f;
        [SerializeField, Tooltip("Maximum rigidbody mass affected.")]
        private float _massCutoff = 40f;
        [SerializeField, Tooltip("Limit of bodies processed per frame to avoid spikes.")]
        private int _maxHits = 12;

        private readonly Collider[] _hits = new Collider[24];

        public void Emit(Vector3 direction, float intensity01, float instability01, float spreadDegrees, ElementDynamicsProfile dynamics)
        {
            var hitCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _hits, ~0, QueryTriggerInteraction.Ignore);
            var accel = dynamics.MaxAcceleration * intensity01;

            for (int i = 0; i < Mathf.Min(hitCount, _maxHits); i++)
            {
                var col = _hits[i];
                var rb = col.attachedRigidbody;
                if (rb == null || rb.mass > _massCutoff)
                {
                    continue;
                }

                var spread = Quaternion.AngleAxis(Random.Range(-spreadDegrees, spreadDegrees) * instability01, Random.onUnitSphere);
                var dir = (spread * direction).normalized;
                rb.AddForce(dir * accel, ForceMode.Acceleration);
            }
        }
    }
}
