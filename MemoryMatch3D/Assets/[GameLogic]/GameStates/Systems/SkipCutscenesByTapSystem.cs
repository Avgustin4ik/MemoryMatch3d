using System.Collections.Generic;
using Entitas;

namespace GameStates.Systems
{
    public class SkipCutscenesByTapSystem : ReactiveSystem<InputEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _pokeballsGroup;

        public SkipCutscenesByTapSystem(IContext<InputEntity> inputContext, GameContext gameContext) : base(inputContext)
        {
            _gameContext = gameContext;
            _pokeballsGroup = _gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.AnimationProcess));
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.TouchDownPosition.Added());
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var gameEntity in _pokeballsGroup.GetEntities())
            {
                gameEntity.isSkipAnimation = true;
            }
            var stateManager = _gameContext.stateManagerEntity;
            if(stateManager.hasTimerAmount == false) return;
            //optional?
            if(stateManager.stateCutscene == false) return;
            if(stateManager.stateLoadingLevel) return;
            stateManager.RemoveTimerAmount();
        }
    }
}