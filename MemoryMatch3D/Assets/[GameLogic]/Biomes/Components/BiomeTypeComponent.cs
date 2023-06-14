using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Biomes
{
    [Game]
    public sealed class BiomeTypeComponent : IComponent
    {
        [EntityIndex] public Biomes value;
    }
}