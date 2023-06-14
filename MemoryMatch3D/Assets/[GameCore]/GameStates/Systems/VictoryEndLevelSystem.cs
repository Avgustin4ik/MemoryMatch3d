using System;
using System.Collections.Generic;
using System.Linq;
using Animals;
using Entitas;

namespace Core.GameStates
{
    public class VictoryEndLevelSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<UiEntity> _victoryScreenGroup;
        private readonly IGroup<UiEntity> _endLevelScreenGroup;
        private readonly DataContext _dataContext;
        private readonly IGroup<DataEntity> _animalDataGroup;
        private readonly AnimalsType _levelAnimalType;

        public VictoryEndLevelSystem(UiContext uiContext, GameContext gameContext, DataContext dataContext) : base(gameContext)
        {
            _victoryScreenGroup = uiContext.GetGroup(UiMatcher.VictoryScreen);
            _endLevelScreenGroup = uiContext.GetGroup(UiMatcher.EndGameLevelsScreen); 
            _dataContext = dataContext;
            _animalDataGroup = dataContext.GetGroup(DataMatcher.AllOf(
                DataMatcher.AnimalType, DataMatcher.UnlockProgress).NoneOf(DataMatcher.SceneLoader));
            _levelAnimalType = dataContext.sceneLoaderEntity.animalType.value;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Victory.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var stateManager in entities)
            {
                stateManager.statePlayerTurn = false;
                if (_dataContext.sceneLoaderEntity != null)
                {
                    var animalData = _animalDataGroup.GetEntities()
                        .FirstOrDefault(e => e.animalType.value == _levelAnimalType);
                    if (animalData == null) throw new ArgumentNullException("Animal data not found");
                    
                    if (animalData != null && animalData.unlockProgress.value >= animalData.unlockGoalLimit.value)
                    {
                        foreach (var uiScreen in _endLevelScreenGroup)
                        {
                            uiScreen.triggerShow = true;
                        }
                        return;
                    }  
                }
                foreach (var uiEntity in _victoryScreenGroup.GetEntities())
                {
                    uiEntity.triggerShow = true;
                }
            }
        }
    }
}