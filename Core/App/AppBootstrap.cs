using UnityEngine;
using XrShared.Core.Services;
using XrShared.UX.StateMachine.States;

namespace XrShared.Core.App
{
    [DisallowMultipleComponent]
    public sealed class AppBootstrap : MonoBehaviour
    {
        [Header("Config")]
        public AppConfig config;

        private ServiceLocator _services;

        private void Awake()
        {
            if (config == null)
            {
                Debug.LogError("[AppBootstrap] Missing AppConfig. Assign it in the inspector.");
                enabled = false;
                return;
            }

            DontDestroyOnLoad(gameObject);

            _services = new ServiceLocator();
            ServiceInstaller.InstallCoreServices(_services, config, transform);

            var sm = _services.Resolve<XrShared.UX.StateMachine.AppStateMachine>();
            sm.SetState(new BootState(sm));
        }

        private void Update()
        {
            if (_services == null) return;
            var sm = _services.Resolve<XrShared.UX.StateMachine.AppStateMachine>();
            sm.Tick(Time.deltaTime);
        }
    }
}
