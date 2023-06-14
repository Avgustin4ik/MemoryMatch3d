using System.Collections.Generic;
using Entitas;

namespace Pokeball
{
    public class BlockInteractionDuringCutscene : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _pokeballsGroup;

        public BlockInteractionDuringCutscene(GameContext gameContext) : base(gameContext)
        {
            _pokeballsGroup = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.IsPokeballOpen));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Cutscene.AddedOrRemoved());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var stateManager in entities)
            {
                foreach (var pokeballEntity in _pokeballsGroup.GetEntities())
                {
                    pokeballEntity.ReplaceInteractable(!stateManager.stateCutscene);
                }
            }
        }
    }
}