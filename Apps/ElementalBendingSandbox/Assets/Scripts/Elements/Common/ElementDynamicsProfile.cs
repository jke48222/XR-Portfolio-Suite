using System;
using UnityEngine;

namespace ElementalBendingSandbox.Elements.Common
{
    /// <summary>
    /// Shared tuning profile for element dynamics. Keeps acceleration and response curves consistent across elements
    /// while allowing distinct mass and damping behaviors.
    /// </summary>
    [Serializable]
    public class ElementDynamicsProfile
    {
        [Tooltip("Max acceleration applied at full intent (m/s^2).")]
        public float MaxAcceleration = 18f;
        [Tooltip("Maximum linear speed allowed for driven rigidbodies (m/s).")]
        public float MaxSpeed = 12f;
        [Tooltip("Time constant for intensity/direction smoothing (seconds).")]
        public float ResponseTime = 0.12f;
        [Tooltip("Extra damping applied when instability grows.")]
        public float InstabilityDamping = 0.35f;

        public float GetLerpFactor(float deltaTime)
        {
            return Mathf.Clamp01(deltaTime / Mathf.Max(ResponseTime, 0.0001f));
        }

        public Vector3 ClampVelocity(Vector3 velocity)
        {
            return Vector3.ClampMagnitude(velocity, MaxSpeed);
        }
    }
}
