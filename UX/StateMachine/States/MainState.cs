namespace XrShared.UX.StateMachine.States
{
    public sealed class MainState : IAppState
    {
        private readonly AppStateMachine _sm;

        public MainState(AppStateMachine sm) { _sm = sm; }

        public void Enter() { }
        public void Exit() { }
        public void Tick(float dt) { }
    }
}
