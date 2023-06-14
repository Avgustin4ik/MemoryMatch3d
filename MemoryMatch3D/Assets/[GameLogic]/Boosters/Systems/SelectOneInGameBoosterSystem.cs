using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Boosters
{
    public class SelectOneInGameBoosterSystem : ReactiveSystem<UiEntity>
    {
        private readonly IGroup<UiEntity> _boostersUiGroup;
        private readonly GameContext _gameContext;

        public SelectOneInGameBoosterSystem(UiContext contextsUI, GameContext contextsGame) : base(contextsUI)
        {
            _boostersUiGroup = contextsUI.GetGroup(UiMatcher.Booster);
            _gameContext = contextsGame;
        }

        protected override ICollector<UiEntity> GetTrigger(IContext<UiEntity> context)
        {
            return context.CreateCollector(UiMatcher.BoosterSelected);
        }

        protected override bool Filter(UiEntity entity)
        {
            return entity.isBooster;
        }

        protected override void Execute(List<UiEntity> entities)
        {
            var all = entities.All(x => x.boosterSelected.value);
            if (all)
            {
                foreach (var boosterEntity in _boostersUiGroup.GetEntities())
                {
                    if(boosterEntity.boosterSelected.value) continue;
                    boosterEntity.ReplaceInteractable(false);
                }
                return;
            }

            if (!all)
            {
                foreach (var uiEntity in _boostersUiGroup.GetEntities())
                {
                    uiEntity.ReplaceInteractable(true);
                    _gameContext.stateManagerEntity.stateBoosterAiming = false;
                }
            }
        
        }
    }
}