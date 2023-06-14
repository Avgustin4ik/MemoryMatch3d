using System.Collections.Generic;
using System.Linq;
using Entitas;
using Ui.Elements;
using UnityEngine;

namespace Boosters
{
    public class ImplementBoosterShowAllSystem : ReactiveSystem<UiEntity>
    {
        private readonly GameContext _gameContext;
        private readonly UiContext _uiContext;
        private readonly IGroup<GameEntity> _pokeballsGroup;

        public ImplementBoosterShowAllSystem(UiContext contextsUI, GameContext contextsGame) : base(contextsUI)
        {
            _uiContext = contextsUI;
            _gameContext = contextsGame;
            _pokeballsGroup = contextsGame.GetGroup(GameMatcher.Pokeball);
        }

        protected override ICollector<UiEntity> GetTrigger(IContext<UiEntity> context)
        {
            return context.CreateCollector(UiMatcher.BoosterImplementationRequest);
        }

        protected override bool Filter(UiEntity entity)
        {
            return entity.boosterType.value == typeof(BoosterOpenAll);
        }

        protected override void Execute(List<UiEntity> entities)
        {
            var boosterEntity = entities[0];
            _gameContext.stateManagerEntity.stateBoosterCutscene = true;
            _gameContext.stateManagerEntity.AddTimerAmount(5);
            foreach (var pokeball in _pokeballsGroup.GetEntities())
            {
                pokeball.triggerShow = true;
                pokeball.isOpenByBooster = true;
            }
        }
    }
}