using System.Collections.Generic;
using Entitas;

namespace Core.DataStorage
{
    public class CheckAnimalAvailableSystem : ReactiveSystem<DataEntity>
    {
        private readonly DataContext _contextsData;

        public CheckAnimalAvailableSystem(DataContext contextsData) : base(contextsData)
        {
            _contextsData = contextsData;
        }

        protected override ICollector<DataEntity> GetTrigger(IContext<DataEntity> context)
        {
            return context.CreateCollector(DataMatcher.UnlockProgress);
        }

        protected override bool Filter(DataEntity entity)
        {
            return entity.hasUnlockProgress;
        }

        protected override void Execute(List<DataEntity> entities)
        {
            foreach (var animalDataEntity in entities)
            {
                var newAnimalState = animalDataEntity.unlockProgress.value >= animalDataEntity.unlockGoalLimit.value;
                if (newAnimalState != animalDataEntity.isAnimalAvailable)
                {
                    var requestEntity = _contextsData.CreateEntity();
                    requestEntity.isRequestUnlockAnimal = true;
                    requestEntity.AddAnimalType(animalDataEntity.animalType.value);
                }
                animalDataEntity.isAnimalAvailable = newAnimalState;
            }
        }
    }
}