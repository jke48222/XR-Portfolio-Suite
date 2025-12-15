using UnityEngine;

namespace ElementalBendingSandbox.Elements.Common
{
    /// <summary>
    /// Compact struct describing the current output of an element controller for downstream physics/VFX.
    /// </summary>
    public struct ElementOutputState
    {
        public readonly ElementState State;
        public readonly Vector3 Direction;
        public readonly float Intensity01;
        public readonly float Stability01;
        public readonly float Confidence01;
        public readonly float Instability01;

        public ElementOutputState(ElementState state, Vector3 direction, float intensity01, float stability01, float confidence01, float instability01)
        {
            State = state;
            Direction = direction;
            Intensity01 = intensity01;
            Stability01 = stability01;
            Confidence01 = confidence01;
            Instability01 = instability01;
        }
    }
}
