using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.Input.Components
{
    [Input, Cleanup(CleanupMode.DestroyEntity)]
    public class ButtonPreviousElementRequestComponent : IComponent
    {
        
    }
}