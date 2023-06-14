using Animals;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.DataStorage.Components
{
    [Data, Game, Level]
    public sealed class AnimalTypeComponent : IComponent
    {
        public AnimalsType value;
    }
}