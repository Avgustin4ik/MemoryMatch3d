using Entitas;

namespace _GameCore_.Common.StateMachine.Components
{
    public sealed class CurrentStateComponent : IComponent
    {
        public IGameStateComponent value;
    }
}