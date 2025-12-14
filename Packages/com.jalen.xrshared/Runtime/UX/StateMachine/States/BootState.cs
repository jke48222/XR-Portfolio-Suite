using XrShared.Core.App;
using UnityEngine.SceneManagement;

namespace XrShared.UX.StateMachine.States
{
    public sealed class BootState : IAppState
    {
        private readonly AppStateMachine _sm;

        public BootState(AppStateMachine sm)
        {
            _sm = sm;
        }

        public void Enter()
        {
            AppConfig config = _sm.Resolve<AppConfig>();
            if (!string.IsNullOrWhiteSpace(config.experienceSceneName))
            {
                SceneManager.LoadSceneAsync(config.experienceSceneName, LoadSceneMode.Additive);
            }

            _sm.SetState(new MainState(_sm));
        }

        public void Exit() { }
        public void Tick(float dt) { }
    }
}
