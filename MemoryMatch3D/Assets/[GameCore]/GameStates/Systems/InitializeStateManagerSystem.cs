using Entitas;

namespace Core.GameStates
{
    public class InitializeStateManagerSystem : IInitializeSystem
    {
        private readonly GameContext gameContext;

        public InitializeStateManagerSystem(GameContext gameContext)
        {
            this.gameContext = gameContext;
        }

        public void Initialize()
        {
            gameContext.isStateManager = true;
        }
    }
}