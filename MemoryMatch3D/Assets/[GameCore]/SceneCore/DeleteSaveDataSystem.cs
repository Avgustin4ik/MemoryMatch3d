using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Core
{
    public class DeleteSaveDataSystem : ReactiveSystem<DataEntity>
    {
        private readonly IGroup<DataEntity> _animalDataGroup;

        public DeleteSaveDataSystem(GameContext gameContext, DataContext contextsData) : base(contextsData)
        {
            _animalDataGroup = contextsData.GetGroup(DataMatcher.AllOf(
                DataMatcher.AnimalType, 
                DataMatcher.UnlockProgress));
        }
    
        protected override ICollector<DataEntity> GetTrigger(IContext<DataEntity> context)
        {
            return context.CreateCollector(DataMatcher.RequestDeleteSaveData.Added());
        }
    
        protected override bool Filter(DataEntity entity)
        {
            return entity.isRequestDeleteSaveData;
        }
    
        protected override void Execute(List<DataEntity> entities)
        {
            foreach (var dataEntity in _animalDataGroup.GetEntities())
            {
                dataEntity.ReplaceUnlockProgress(0);
            }
            PlayerPrefs.DeleteAll();
#if UNITY_EDITOR
            Debug.Log("Animal progress was deleted)");
#endif
        }
    }
}