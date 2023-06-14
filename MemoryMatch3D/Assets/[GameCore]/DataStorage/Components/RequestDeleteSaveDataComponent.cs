using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.DataStorage.Components
{
    [Data, Cleanup(CleanupMode.DestroyEntity)]
    public sealed class RequestDeleteSaveDataComponent : IComponent
    {
        
    }
}