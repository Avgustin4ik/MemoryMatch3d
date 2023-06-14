using System.Collections.Generic;
using Entitas;

namespace Pokeball
{
    public class InitializePokeballsOnFieldSystem : ReactiveSystem<GameEntity>
    {
        
        public void Initialize()
        {
            var gameObjects = UnityEngine.Object.FindObjectsOfType<PokeballView>();
            foreach (var gameObject in gameObjects)
            {
                var pokeballEntity = Contexts.sharedInstance.game.CreateEntity();
                gameObject.Init(pokeballEntity);
            }
        }

        public InitializePokeballsOnFieldSystem(IContext<GameEntity> gameContext) : base(gameContext)
        {
            
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector( GameMatcher.LoadingLevel.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            Initialize();
        }
    }
}