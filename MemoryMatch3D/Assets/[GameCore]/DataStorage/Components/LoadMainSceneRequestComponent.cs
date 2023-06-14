using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.DataStorage.Components
{
    [Game, Cleanup(CleanupMode.DestroyEntity)]
    public class LoadMainSceneRequestComponent : IComponent
    {
        
    }
}