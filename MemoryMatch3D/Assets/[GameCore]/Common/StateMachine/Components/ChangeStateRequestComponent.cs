using Entitas;

namespace _GameCore_.Common.StateMachine.Components
{
    [Game]
    public sealed class ChangeStateRequestComponent : IComponent
    {
        public IGameStateComponent newState;
    }
}