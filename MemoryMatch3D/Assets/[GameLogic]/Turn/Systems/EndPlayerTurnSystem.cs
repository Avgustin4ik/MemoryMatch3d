using System.Collections.Generic;
using System.Linq;
using Entitas;
using Unity.Mathematics;
using UnityEngine;

namespace Turn
{
    public class EndPlayerTurnSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _playerGroup;
        private readonly UiContext _uiContext;
        private GameContext _gameContext;
        private readonly IGroup<GameEntity> _pokeballGroup;

        public EndPlayerTurnSystem(GameContext contextsGame) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _uiContext = Contexts.sharedInstance.ui;
            _playerGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.Player,
                GameMatcher.PlayerID,
                GameMatcher.TurnOrder));
            _pokeballGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.IsPokeballOpen));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.EndTurnRequest);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            //todo endturn logic
            foreach (var pokeball in _pokeballGroup)
            {
                pokeball.ReplaceInteractable(true);
            }
            //todo check if is not end level
            var playerEntities = _playerGroup.GetEntities();
            _uiContext.CreateEntity().isTurnEndEventUI = true;
            
            var currentTurnPlayer = playerEntities.First(player => player.isThisPlayersTurn);
            var turnOrderValue = currentTurnPlayer.turnOrder.value + 1 > playerEntities.Length ? 1 : currentTurnPlayer.turnOrder.value + 1;
            if (playerEntities.Length == 1) turnOrderValue = 1;
            currentTurnPlayer.isThisPlayersTurn = false;
            playerEntities.First(player => player.turnOrder.value == turnOrderValue).isThisPlayersTurn = true;

            _gameContext.turnControllerEntity.ReplaceTotalNumberOfCompletedTurns(
                _gameContext.turnControllerEntity.totalNumberOfCompletedTurns.value + 1);
        }
    }
}