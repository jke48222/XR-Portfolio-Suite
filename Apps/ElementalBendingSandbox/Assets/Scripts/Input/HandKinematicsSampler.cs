using System;
using UnityEngine;
using UnityEngine.XR;

namespace ElementalBendingSandbox.Input
{
    /// <summary>
    /// Samples XR hand/controller pose and velocity with temporal smoothing and noise rejection.
    /// Designed to run in Update for freshest data; downstream physics uses FixedUpdate with cached output.
    /// </summary>
    public class HandKinematicsSampler : MonoBehaviour
    {
        [Serializable]
        public class HandSample
        {
            public XRNode Node = XRNode.LeftHand;
            [Range(0.01f, 0.5f)] public float VelocitySmoothing = 0.1f;
            [Range(0.0f, 2.0f)] public float DeadzoneVelocity = 0.05f; // m/s before we consider motion meaningful.

            public Vector3 SmoothedVelocity { get; private set; }
            public Vector3 Position { get; private set; }
            public bool IsTracked { get; private set; }

            private InputDevice _device;
            private bool _deviceValid;

            public void TryInitializeDevice()
            {
                _device = InputDevices.GetDeviceAtXRNode(Node);
                _deviceValid = _device.isValid;
            }

            public void Tick(float deltaTime)
            {
                if (!_deviceValid || !_device.isValid)
                {
                    TryInitializeDevice();
                    if (!_deviceValid)
                    {
                        IsTracked = false;
                        SmoothedVelocity = Vector3.zero;
                        return;
                    }
                }

                IsTracked = _device.TryGetFeatureValue(CommonUsages.devicePosition, out var position);
                if (IsTracked)
                {
                    Position = position;
                }

                if (_device.TryGetFeatureValue(CommonUsages.deviceVelocity, out var velocity))
                {
                    // Exponential smoothing to limit noise without adding latency.
                    var targetVelocity = velocity.magnitude < DeadzoneVelocity ? Vector3.zero : velocity;
                    SmoothedVelocity = Vector3.Lerp(SmoothedVelocity, targetVelocity, Mathf.Clamp01(deltaTime / Mathf.Max(VelocitySmoothing, 0.0001f)));
                }
                else
                {
                    SmoothedVelocity = Vector3.zero;
                }
            }
        }

        public HandSample LeftHand = new HandSample { Node = XRNode.LeftHand };
        public HandSample RightHand = new HandSample { Node = XRNode.RightHand };

        public HandSample GetHand(XRNode node) => node == XRNode.LeftHand ? LeftHand : RightHand;

        private void Awake()
        {
            // Initialize immediately to avoid first-frame latency.
            LeftHand.TryInitializeDevice();
            RightHand.TryInitializeDevice();
        }

        private void Update()
        {
            // Update uses real deltaTime for tighter responsiveness. Consumers pull cached values in FixedUpdate.
            var dt = Time.deltaTime;
            LeftHand.Tick(dt);
            RightHand.Tick(dt);
        }
    }
}
