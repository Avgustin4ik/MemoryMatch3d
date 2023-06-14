using System.Collections.Generic;
using System.Linq;
using Animals;
using Core.Configs;
using Entitas;

namespace Pokeball
{
    public class PokeballsMatchLogicSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _pokeballsGroup;
        private readonly int _reward;
        private readonly IGroup<GameEntity> _animalsGroup;
        private readonly IGroup<GameEntity> _cellGroup;

        public PokeballsMatchLogicSystem(GameContext gameContext) : base(gameContext)
        {
            _reward = ConfigsCatalogsManager.GetConfig<GameConfig>().SoftRewardSuccessCompare;
            _gameContext = gameContext;
            _cellGroup = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Cell,
                GameMatcher.LinkedPokeball));
            _pokeballsGroup = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.IsPokeballOpen));
            _animalsGroup = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Animal,
                GameMatcher.IsVisible));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(
                GameMatcher.MatchEvent));
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            if (!Contexts.sharedInstance.game.HasEntity(_gameContext.stateManagerEntity)) return; 
            if (_gameContext.stateManagerEntity.stateBoosterCutscene) return;
            if (!_gameContext.stateManagerEntity.stateMainGame) return;

            _gameContext.stateManagerEntity.stateSuccessCompare = true;
            _gameContext.stateManagerEntity.stateComplicatorsImplementation = true;
            
            foreach (var pokeballEntity in _pokeballsGroup.GetEntities())
            {
                if (!pokeballEntity.isMatchDetected) continue;
                var cell = _cellGroup.GetEntities().First(cell => cell.linkedPokeball.value == pokeballEntity.hashCode.value);
                cell.RemoveLinkedPokeball();
                var animalEntity = _animalsGroup.GetEntities().FirstOrDefault(al => al.hashCode.value == pokeballEntity.linkedAnimalHashcode.value);
                animalEntity.isAnimalFree = true;
                pokeballEntity.Destroy();
            }
            var userData = _gameContext.userDataEntity;
            userData.ReplaceUserDataMoney(userData.userDataMoney.value + _reward);
        }
    }
}