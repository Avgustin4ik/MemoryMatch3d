using System.Linq;
using Core.Configs;
using Entitas;
// using Services.GameLevelsLoadRule;

namespace Core.GameLevels
{
    public sealed class InitializeGameLevelsSystem : IInitializeSystem
    {
        private readonly LevelContext _levelContext;
        private readonly IGroup<DataEntity> _dataGroup;
        private readonly DataContext _dataContext;
        private readonly IGroup<LevelEntity> _blueprintsGroup;

        public InitializeGameLevelsSystem(GameContext gameContext, LevelContext levelContext, DataContext dataContext)
        {
            _levelContext = levelContext;
            _dataGroup = dataContext.GetGroup(DataMatcher.AllOf(
                DataMatcher.UnlockProgress,
                DataMatcher.AnimalType));
            _dataContext = dataContext;
            _blueprintsGroup = levelContext.GetGroup(LevelMatcher.AllOf(
                LevelMatcher.Blueprint));
        }

        public void Initialize()
        {
            var levelLoader = _levelContext.levelLoaderEntity ?? _levelContext.CreateEntity();
            levelLoader.isLevelLoader = true;
            var unlockProgress = 0;
            if (_dataContext.sceneLoaderEntity != null)
            {
                var currentAnimalTypeLevel = _dataContext.sceneLoaderEntity.animalType.value;
                unlockProgress = _dataGroup.GetEntities().First(e => e.animalType.value == currentAnimalTypeLevel)
                    .unlockProgress.value;
            }
            levelLoader.ReplaceNextLevelNumber(unlockProgress); //будет загружен первый уровень
        }
    }
}