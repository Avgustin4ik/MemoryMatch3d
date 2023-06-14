using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Core
{
    public class SaveAnimalUnlockProgressSystem : ReactiveSystem<DataEntity>
    {
        public SaveAnimalUnlockProgressSystem(DataContext contextsData) : base(contextsData)
        {
            
        }

        protected override ICollector<DataEntity> GetTrigger(IContext<DataEntity> context)
        {
            return context.CreateCollector(DataMatcher.UnlockProgress);
        }

        protected override bool Filter(DataEntity entity)
        {
            return entity.hasAnimalType;
        }

        protected override void Execute(List<DataEntity> entities)
        {
            foreach (var dataEntity in entities)
            {
                var unlockProgressValue = dataEntity.unlockProgress.value;
                var key = dataEntity.animalType.value.ToString();
                PlayerPrefs.SetInt(key,unlockProgressValue);
            }
        }
    }
}