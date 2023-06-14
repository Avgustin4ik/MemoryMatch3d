using Entitas;

namespace DebugMenu
{
    public class DebugMenuInitializeSystem : IInitializeSystem
    {
        public DebugMenuInitializeSystem(GameContext contextsGame, UiContext contextsUI)
        {
        }

        public void Initialize()
        {
            var gameEntity = Contexts.sharedInstance.game.CreateEntity();
            gameEntity.isDebugManager = true;
            gameEntity.isDisplayFPSAlways = false;
        }

    }
}