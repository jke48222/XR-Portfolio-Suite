using UnityEngine;
using XrShared.Core.Services;
using XrShared.Core.Tick;

namespace XrShared.Core.Diagnostics
{
    /// <summary>
    /// Lightweight developer-only runtime overlay for FPS and core counters.
    /// Enabled only in editor and development builds.
    /// </summary>
    public sealed class DebugOverlay : MonoBehaviour, ITickable
    {
        private ServiceLocator _services;
        private DebugStats _stats;

        private bool _visible = true;
        private GUIStyle _style;

        private int _frameCount;
        private float _timeAccum;
        private long _lastMonoMemory;

        public void Initialize(ServiceLocator services)
        {
            _services = services;
            _stats = services.Resolve<DebugStats>();

            _style = new GUIStyle
            {
                fontSize = 18,
                normal = { textColor = Color.white }
            };

            _lastMonoMemory = System.GC.GetTotalMemory(false);
        }

        public void Tick(float dt)
        {
            if (Input.GetKeyDown(KeyCode.F3))
                _visible = !_visible;

            _frameCount++;
            _timeAccum += dt;

            if (_timeAccum >= 0.5f)
            {
                _stats.fps = _frameCount / _timeAccum;
                _frameCount = 0;
                _timeAccum = 0f;
            }

            long current = System.GC.GetTotalMemory(false);
            _stats.managedAllocBytesPerFrame = Mathf.Max(0, (int)(current - _lastMonoMemory));
            _lastMonoMemory = current;

            _stats.ResetFrameStats();
        }

        private void OnGUI()
        {
            if (!_visible) return;

            GUI.Label(
                new Rect(16, 16, 1400, 40),
                _stats.ToString(),
                _style
            );

            GUI.Label(
                new Rect(16, 44, 1400, 30),
                "F3 toggles debug overlay (editor / dev builds)",
                _style
            );
        }
    }
}
