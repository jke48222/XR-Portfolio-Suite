using UnityEngine;
using XrShared.Core.App;
using XrShared.Core.Diagnostics;
using XrShared.Core.Performance;
using XrShared.Core.Capture;
using XrShared.Core.Data;
using XrShared.XR.Input;
using XrShared.MR.Anchors;
using XrShared.MR.Compositing;
using XrShared.Audio.Features;
using XrShared.UX.StateMachine;

namespace XrShared.Core.Services
{
    public static class ServiceInstaller
    {
        public static void InstallCoreServices(ServiceLocator services, AppConfig config, Transform root)
        {
            services.Register(config);

            services.Register<IPersistenceStore>(
                new JsonPersistenceStore(config.saveFileName, config.saveSchemaVersion));

            services.Register<PerformanceGovernor>(
                new PerformanceGovernor(config.performanceConfig));

            services.Register<CaptureController>(
                new CaptureController(config.captureConfig));

            services.Register<DebugStats>(new DebugStats());
            services.Register<EventBus>(new EventBus());

            services.Register<IInputRouter>(new NullInputRouter());
            services.Register<IAnchorStore>(new NullAnchorStore());
            services.Register<IPassthroughController>(new NullPassthroughController());
            services.Register<IOcclusionController>(new NullOcclusionController());
            services.Register<IAudioFeatures>(new UnityAudioFeatures());

            services.Register<AppStateMachine>(new AppStateMachine(services));

            AttachMonoBehaviours(root, services, config);
        }

        private static void AttachMonoBehaviours(Transform root, ServiceLocator services, AppConfig config)
        {
            Tick.TickRunner tickRunner = root.gameObject.AddComponent<Tick.TickRunner>();
            tickRunner.Initialize(services);

            if (Core.App.BuildInfo.IsDevBuild && config.enableDebugOverlayInDev)
            {
                DebugOverlay overlay = root.gameObject.AddComponent<DebugOverlay>();
                overlay.Initialize(services);
            }
        }
    }
}
