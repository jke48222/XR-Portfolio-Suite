namespace XrShared.UX.StateMachine
{
    public interface IAppState
    {
        void Enter();
        void Exit();
        void Tick(float dt);
    }
}
