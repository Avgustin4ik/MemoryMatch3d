using System.Collections.Generic;
using Configs;
using Core.Configs;
using Entitas;

namespace Player
{
    public class InitializePlayersSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly PlayersConfig _playerConfig;

        public InitializePlayersSystem(GameContext contextsGame) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _playerConfig = ConfigsCatalogsManager.GetConfig<PlayersConfig>();
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.UserData.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            short order = 1;
            foreach (var playerData in _playerConfig.PlayersCollection)
            {
                var gameEntity = _gameContext.CreateEntity();
                gameEntity.isPlayer = true;
                gameEntity.AddPlayerName(playerData.Name);
                gameEntity.AddPlayerID(playerData.ID);
                gameEntity.AddTurnOrder(order++);
                gameEntity.AddPlayersTurnsLimit(playerData.TurnsLimit);
                gameEntity.AddPlayerTurnsLeft(playerData.TurnsLimit);
            }
        }
    }
}