using XrShared.Core.Services;

namespace XrShared.UX.StateMachine
{
    public sealed class AppStateMachine
    {
        private readonly ServiceLocator _services;
        private IAppState _current;

        public AppStateMachine(ServiceLocator services)
        {
            _services = services;
        }

        public void SetState(IAppState next)
        {
            _current?.Exit();
            _current = next;
            _current?.Enter();
        }

        public void Tick(float dt)
        {
            _current?.Tick(dt);
        }

        public T Resolve<T>() where T : class => _services.Resolve<T>();
    }
}
