using System.Collections.Generic;
using System.Linq;
using Entitas;


namespace Turn
{
    public class InitializeTurnControllerSystem : ReactiveSystem<LevelEntity>, IInitializeSystem
    {
        private readonly IGroup<LevelEntity> _blueprintsGroup;
        private readonly LevelContext _levelContext;
        private readonly GameContext _gameConxtext;

        public InitializeTurnControllerSystem(LevelContext levelContext, GameContext contextsGame) : base(levelContext)
        {
            _blueprintsGroup = levelContext.GetGroup(LevelMatcher.AllOf(
                LevelMatcher.Blueprint,
                LevelMatcher.TurnsCount));
            _levelContext = levelContext;
            _gameConxtext = contextsGame;
        }

        public void Initialize()
        {
            var turnController = Contexts.sharedInstance.game.CreateEntity();
            turnController.isTurnController = true;
            turnController.AddTotalNumberOfCompletedTurns(0);
        }

        protected override ICollector<LevelEntity> GetTrigger(IContext<LevelEntity> context)
        {
            return context.CreateCollector(LevelMatcher.LoadingComplete.Added());
        }

        protected override bool Filter(LevelEntity entity)
        {
            return true;
        }

        protected override void Execute(List<LevelEntity> entities)
        {
            var levelEntity = _levelContext.GetEntitiesWithBlueprint(_levelContext.levelLoaderEntity.currentLevelNumber.value).First();
            var turnControllerEntity = _gameConxtext.turnControllerEntity;
            turnControllerEntity.AddTurnsLimitByLevel(levelEntity.turnsCount.value);
            turnControllerEntity.isTurnCounterDisabled = levelEntity.isTurnsEndless;
        }
    }
}