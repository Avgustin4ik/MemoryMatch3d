using Entitas;

namespace Boosters
{
    [Game]
    public sealed class BoosterComponent : IComponent
    {
        
    }

    [Game]
    public sealed class BoosterInventoryComponent : IComponent
    {
        public BoosterInventory value;
    }

    [Game]
    public sealed class AimTargetComponent : IComponent
    {
        public AimTarget value;
    }

    [Game]
    public sealed class BoosterUsedFlagComponent : IComponent
    {
        
    }


    public enum AimTarget
    {
        All = 0,
        One = 1,
        Column = 2,
        Raw = 3,
        OneWithPokeball = 4
    }

    [Game]
    public sealed class BoostersEndlessComponent : IComponent
    {
        
    }
}