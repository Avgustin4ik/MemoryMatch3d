using System.Collections.Generic;
using System.Linq;
using Entitas;
using Ui.Elements;

namespace Boosters
{
    public class ImplementBoosterOpenOneMoreSystem : ReactiveSystem<UiEntity>
    {
        private readonly IGroup<GameEntity> _cellGroup;
        private readonly IGroup<GameEntity> _pokeballsGroup;

        public ImplementBoosterOpenOneMoreSystem(UiContext contextsUI, GameContext contextsGame) : base(contextsUI)
        {
            _cellGroup = contextsGame.GetGroup(GameMatcher.Cell);
            _pokeballsGroup = contextsGame.GetGroup(GameMatcher.Pokeball);
        }

        protected override ICollector<UiEntity> GetTrigger(IContext<UiEntity> context)
        {
            return context.CreateCollector(UiMatcher.BoosterImplementationRequest);
        }

        protected override bool Filter(UiEntity entity)
        {
            return entity.boosterType.value == typeof(BoosterOpenOneMore);
        }

        protected override void Execute(List<UiEntity> entities)
        {
            foreach (var requestEntity in entities)
            {
                foreach (var cellEntity in _cellGroup.GetEntities())
                {
                    if(!cellEntity.inFocus.value) continue;
                    var pokeball = _pokeballsGroup.GetEntities().FirstOrDefault(p=>p.hashCode.value == cellEntity.linkedPokeball.value);
                    pokeball.triggerShow = true;
                    pokeball.isOpenByBooster = true;
                }
                //todo booster vfx
            }
        }
    }
}