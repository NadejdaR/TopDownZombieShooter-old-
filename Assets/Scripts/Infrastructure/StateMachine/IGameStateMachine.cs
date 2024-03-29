using TDZS.Infrastructure.StateMachine.State;

namespace TDZS.Infrastructure.StateMachine
{
  public interface IGameStateMachine
  {
    void Enter<TState>() where TState : IState;
  }
}