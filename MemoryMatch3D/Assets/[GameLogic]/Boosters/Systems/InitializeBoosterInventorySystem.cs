using System.Collections.Generic;
using Entitas;
using Ui.Elements;

namespace Boosters
{
    public class InitializeBoosterInventorySystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;

        public InitializeBoosterInventorySystem(GameContext contextsGame) : base(contextsGame)
        {
            _gameContext = contextsGame;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Player.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isPlayer;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var playerEntity in entities)
            {
                playerEntity.AddBoosterInventory(new BoosterInventory());
                playerEntity.boosterInventory.value.Add(typeof(BoosterOpenOneMore));
                playerEntity.boosterInventory.value.Add(typeof(BoosterOpenAll));
                
            }
        }
    }
}