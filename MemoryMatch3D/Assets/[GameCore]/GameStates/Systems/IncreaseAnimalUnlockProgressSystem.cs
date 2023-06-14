using System;
using System.Collections.Generic;
using System.Linq;
using Animals;
using Entitas;

namespace Core.GameStates
{
    public class IncreaseAnimalUnlockProgressSystem : ReactiveSystem<GameEntity
    >
    {
        private readonly DataContext _dataContext;
        private readonly GameContext _gameContext;
        private readonly IGroup<DataEntity> _animalsDataGroup;
        private readonly AnimalsType _levelAnimalType;

        public IncreaseAnimalUnlockProgressSystem(GameContext contextsGame, DataContext contextsData) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _dataContext = contextsData; 
            _animalsDataGroup = _dataContext.GetGroup(DataMatcher.AllOf(DataMatcher.AnimalType,
                    DataMatcher.UnlockProgress)
                .NoneOf(DataMatcher.SceneLoader));
            if (_dataContext.sceneLoaderEntity.hasAnimalType == false)
                throw new NotImplementedException("No animal type in scene loader entity");
            _levelAnimalType = _dataContext.sceneLoaderEntity.animalType.value;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Victory.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return _dataContext.sceneLoaderEntity != null;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var animalData = _animalsDataGroup.GetEntities().FirstOrDefault(x => x.animalType.value == _levelAnimalType);
            if(animalData == null)
                throw new Exception("Animal data not found");
            animalData.ReplaceUnlockProgress(animalData.unlockProgress.value + 1);
        }
    }
}