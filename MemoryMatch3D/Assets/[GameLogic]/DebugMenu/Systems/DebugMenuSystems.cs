namespace DebugMenu
{
    public class DebugMenuSystems : Feature
    {
        public DebugMenuSystems(Contexts contexts)
        {
            //todo fix by BUILD
            // Add(new OpenDebugMenuByTouchSystem(contexts.input, contexts.ui));
            Add(new OpenDebugMenuByButton(contexts.input, contexts.ui));
            Add(new CloseDebugMenuSystem(contexts.input, contexts.game));
            // Add(new ShowFPSStatsSystem(contexts.game, contexts.ui));
            Add(new DebugMenuInitializeSystem(contexts.game, contexts.ui));
        }

    }
}