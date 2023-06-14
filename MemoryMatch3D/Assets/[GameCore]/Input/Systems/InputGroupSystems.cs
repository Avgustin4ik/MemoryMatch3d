namespace Core.Input
{
    public sealed class InputGroupSystems : Entitas.Systems
    {
        public InputGroupSystems(Contexts contexts)
        {
            Add(new TouchDetectSystems(contexts.input));
            Add(new RaycastSystem(contexts.input, contexts.game));
        }
    }
}