using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Entitas;
using UnityEngine;

namespace Pokeball
{
    public class BlockPokeballInteractionByLimit : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _pokeballsGroup;
        private readonly int _pokeballsLimit;
        private readonly IGroup<GameEntity> _onOpenPokeballsGroup;
        private readonly IGroup<UiEntity> _boosterGroup;

        public BlockPokeballInteractionByLimit(GameContext gameContext, UiContext uiContext) : base(gameContext)
        {
            _pokeballsGroup = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.IsPokeballOpen,
                GameMatcher.Pokeball));
            _pokeballsLimit = ConfigsCatalogsManager.GetConfig<GameConfig>().OpenItemsLimit;
            _onOpenPokeballsGroup = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.AnimationProcess));
            _boosterGroup = uiContext.GetGroup(UiMatcher.AllOf(
                UiMatcher.Booster,
                UiMatcher.Interactable));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Clicked.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isPokeball
                && entity.interactable.value == true
                && entity.isPokeballOpen.value == false;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var countClickedPokeballs = entities.Count;
            var countOpenedPokeballs = _pokeballsGroup.GetEntities().Count(e => e.isPokeballOpen.value && e.isOpenByBooster == false && e.isAnimationProcess == false);
            int countOnOpenProgressPokeballs = _onOpenPokeballsGroup.GetEntities().Count(e => e.isPokeballOpen.value == false);
            if(countClickedPokeballs + countOpenedPokeballs + countOnOpenProgressPokeballs < _pokeballsLimit) return;
            foreach (var gameEntity in _pokeballsGroup.GetEntities())
            {
                gameEntity.ReplaceInteractable(false);
            }

            foreach (var booster in _boosterGroup)
            {
                booster.ReplaceInteractable(false);
            }
        }
    }
}