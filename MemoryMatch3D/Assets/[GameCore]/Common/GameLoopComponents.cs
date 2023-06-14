using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.Common
{
    [Game, Unique, FlagPrefix("isStatic")]
    public sealed class CurrentGameComponent : IComponent
    {
    }

    [Game]
    public sealed class GameStartedComponent : IComponent
    {
    }
}