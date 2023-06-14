using Animals;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Biomes
{
    [Game]
    public class BiomeZoneComponent : IComponent
    {
        [PrimaryEntityIndex] public AnimalsType value;
    }
}