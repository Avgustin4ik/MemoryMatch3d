using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Entitas;

namespace Pokeball
{
    public class IncreaseAnimationSpeedByClickSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _animatedViewsGroup;
        private readonly IGroup<GameEntity> _pokeballsGroup;
        private readonly int _pokeballsLimit;
        private readonly IGroup<GameEntity> _onOpenPokeballsGroup;

        public IncreaseAnimationSpeedByClickSystem(GameContext contextsGame) : base(contextsGame)
        {
            _animatedViewsGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.AnimationProcess).NoneOf(
                GameMatcher.AnimationRewind));
            _pokeballsGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.IsPokeballOpen,
                GameMatcher.Pokeball));
            _pokeballsLimit = ConfigsCatalogsManager.GetConfig<GameConfig>().OpenItemsLimit;
            _onOpenPokeballsGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.AnimationProcess));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Clicked.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isCell;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in _animatedViewsGroup.GetEntities())
            {
                entity.isAnimationRewind = true;
            }
        }
    }
}