using System.Collections.Generic;
using Entitas;

namespace Core.GameLevels
{
    public sealed class GameLevelsSystems : Systems
    {
        public GameLevelsSystems(Contexts contexts)
        {

            Add(new InitializeGameLevelsSystem(contexts.game, contexts.level, contexts.data));
            Add(new LoadLevelSystem(contexts.level, contexts.game));
            Add(new DestroyLevelLoaderSystem(contexts.level));
        }

      
    }

    public class DestroyLevelLoaderSystem : ITearDownSystem
    {
        private readonly LevelContext _levelContext;

        public DestroyLevelLoaderSystem(LevelContext contextsLevel)
        {
            _levelContext = contextsLevel;
        }

        public void TearDown()
        {
            _levelContext.levelLoaderEntity?.Destroy();
        }
    }
}