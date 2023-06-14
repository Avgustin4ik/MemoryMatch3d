using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.UsedData
{
    public class UserDataComponents
    {
        [Game, Unique]
        public sealed class UserDataComponent : IComponent
        {
        }

        [Game, Event(EventTarget.Any)]
        public sealed class UserDataMoneyComponent : IComponent
        {
            public int value;
        }

        [Game]
        public sealed class CurrentLevelComponent : IComponent
        {
            public int value;
        }

        [Game]
        public sealed class OpenedLevelComponent : IComponent
        {
            public int value;
        }
        
        
        
    }
}