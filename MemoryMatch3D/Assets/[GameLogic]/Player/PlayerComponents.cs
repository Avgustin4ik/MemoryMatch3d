using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Player
{
    [Game]
    public sealed class PlayerComponent : IComponent
    {
        
    }

    [Game]
    public sealed class PlayerNameComponent : IComponent
    {
        public string value;
    }

    [Game]
    public sealed class PlayerIDComponent : IComponent
    {
        public int value;
    }

    [Game, Unique]
    public sealed class PlayersDataComponent : IComponent
    {
        
    }

    [Game]
    public sealed class PlayersTurnsLimitComponent : IComponent
    {
        public uint value;
    }

    [Game]
    public sealed class PlayerTurnsLeftComponent : IComponent
    {
        public uint value;
    }
    
    
    
}