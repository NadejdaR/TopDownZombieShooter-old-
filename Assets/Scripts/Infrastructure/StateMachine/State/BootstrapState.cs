using TDZS.Game.Input;
using TDZS.Infrastructure.SceneLoading;
using TDZS.Utility;
using TDZS.Utility.Constants;

namespace TDZS.Infrastructure.StateMachine.State
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly Services.Services _services;

        public BootstrapState(IGameStateMachine stateMachine, Services.Services services,
            ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _services = services;

            RegisterServices(coroutineRunner);
        }

        public void Enter()
        {
            UnityEngine.Debug.Log($"Enter BootstrapState Frame <{UnityEngine.Time.frameCount}>");
            LoadMenuScene();
        }

        public void Exit()
        {
            UnityEngine.Debug.Log($"Exit BootstrapState Frame <{UnityEngine.Time.frameCount}>");
        }

        private void RegisterServices(ICoroutineRunner coroutineRunner)
        {
            _services.Register<IInputService>(new StandardInputService());
            _services.Register<ICoroutineRunner>(coroutineRunner);
            _services.Register<ISceneLoader>(new AsyncSceneLoader(_services.Get<ICoroutineRunner>()));
        }

        private void LoadMenuScene()
        {
            ISceneLoader sceneLoader = _services.Get<ISceneLoader>();  
            sceneLoader.Load(SceneName.Menu, EnterMenuState);
        }

        private void EnterMenuState()
        {
            _stateMachine.Enter<MenuState>();
        }
    }
}
