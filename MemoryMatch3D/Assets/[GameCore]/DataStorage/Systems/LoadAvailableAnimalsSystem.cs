using System;
using System.Linq;
using Animals;
using Entitas;
using UnityEngine;

namespace Core.DataStorage
{
    public class LoadAvailableAnimalsSystem : IInitializeSystem
    {
        private readonly DataContext _dataContext;
        private readonly IGroup<LevelEntity> _blueprintGroup;

        public LoadAvailableAnimalsSystem(DataContext dataContext, LevelContext levelContext)
        {
            _dataContext = dataContext;
            _blueprintGroup = levelContext.GetGroup(LevelMatcher.AllOf(
                LevelMatcher.Blueprint,
                LevelMatcher.AnimalType));
        }

        public void Initialize()
        {
            var types = Enum.GetValues(typeof(AnimalsType));
            foreach (var animalType in types)
            {
                var dataEntity = _dataContext.CreateEntity();
                var animalsType = (AnimalsType) animalType;
                dataEntity.AddAnimalType(animalsType);
                dataEntity.AddUnlockGoalLimit(GetUnlockLimit(animalsType)); //todo magic number
                var key = animalType.ToString(); 
                dataEntity.AddUnlockProgress(PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : 0);
                if (animalsType == AnimalsType.GoldFish ||
                    animalsType == AnimalsType.Tortoise)
                {
                    dataEntity.ReplaceUnlockProgress(dataEntity.unlockGoalLimit.value);
                    dataEntity.isAnimalAvailable = true;
                }
            }
        }

        private int GetUnlockLimit(AnimalsType animalsType) => _blueprintGroup.GetEntities().Count(x => x.animalType.value.Equals(animalsType));
    }
}