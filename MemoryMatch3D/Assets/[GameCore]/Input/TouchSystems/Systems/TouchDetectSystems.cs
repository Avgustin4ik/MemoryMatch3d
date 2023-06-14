namespace Core.Input
{
    public sealed class TouchDetectSystems : Entitas.Systems
    {
        public TouchDetectSystems(InputContext inputContext)
        {
            Add(new TouchSystem(inputContext));
            Add(new SwipeSystem(inputContext));
        }
    }
}