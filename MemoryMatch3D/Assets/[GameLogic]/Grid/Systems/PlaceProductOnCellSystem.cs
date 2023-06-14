using System;
using System.Collections.Generic;
using System.Linq;
using Animals;
using Biomes;
using Core.Configs;
using Entitas;
using Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Grid
{
    public class PlaceProductOnCellSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly GameLevelsPrefabsConfig _levelConfig;
        private readonly LevelContext _levelContext;
        private readonly IGroup<GameEntity> _cellGroup;
        private readonly BiomeCatalog _houseBiomeAnimalCatalog;
        private readonly IGroup<DataEntity> _availableAnimalsGroup;
        private readonly GameConfig _gameConfig;

        public PlaceProductOnCellSystem(GameContext contextsGame, LevelContext contextsLevel, DataContext contextData) : base(contextsGame)
        {
            _availableAnimalsGroup = contextData.GetGroup(DataMatcher.AllOf(
                DataMatcher.AnimalAvailable,
                DataMatcher.AnimalType));
            _gameContext = contextsGame;
            _levelContext = contextsLevel;
            _levelConfig = ConfigsCatalogsManager.GetConfig<GameLevelsPrefabsConfig>();
            _cellGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.Cell,
                GameMatcher.Index));
            _gameConfig = ConfigsCatalogsManager.GetConfig<GameConfig>();
            _houseBiomeAnimalCatalog = _gameConfig.GetBiomeCatalog(Biomes.Biomes.House);
            
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.GridComplete);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var curLevelNumber = _levelContext.levelLoaderEntity.currentLevelNumber.value;
            var blueprintEntity = _levelContext.GetEntitiesWithBlueprint(curLevelNumber).FirstOrDefault();
            var animals = new List<AnimalView>();
            Debug.Assert(blueprintEntity != null, nameof(blueprintEntity) + " != null");
            var cellContent = blueprintEntity.cellContent.value.Cast<int>().ToArray();
            const int animal = (int)LevelBlueprintData.CellContentEnum.Animal;
            var count = cellContent.Count(x => x == animal);
            Debug.Assert(count % 2 == 0,"Non odd animals count in storage from JSON. Animals count was increased by one");
            if (count % 2 != 0) count++;
            var arrayLength = count / 2;
            var animalsTypes = _availableAnimalsGroup.GetEntities()
                .Select(e => e.animalType.value)
                .Shuffle();
            for (var i = 0; i < arrayLength; i++)
            {
                Debug.Assert(animalsTypes != null && animalsTypes.Any(), "No available animals");
                var type = animalsTypes.ElementAt(i % animalsTypes.Count());
                var randomAnimalView = _houseBiomeAnimalCatalog.GetAnimal(type);
                animals.Add(randomAnimalView);
                animals.Add(randomAnimalView);
            }
            
            var randomizedAnimals = animals.Shuffle().ToList();
            var index = 0;
            var cellEntities = Enumerable.OrderBy(_cellGroup.GetEntities(), cell => cell.index.value.x)
                .ThenBy(cell => cell.index.value.y).ToArray();
            for (int i = 0; i < cellEntities.Length; i++)
            {
                if(!cellContent[i].Equals(animal)) continue;
                InstantiatePokeball(animal: randomizedAnimals[index], cellEntities[i]);
                index++;
            }
        }

        private void InstantiatePokeball(AnimalView animal, GameEntity cellEntity)
        {
            var gameObject = Object.Instantiate(_gameConfig.PokeballPrefab, cellEntity.transform.value.position,
                Quaternion.identity);
            var gameEntity = _gameContext.CreateEntity();
            gameObject.Init(gameEntity);
            cellEntity.AddLinkedPokeball(gameEntity.hashCode.value);
            gameObject.SetAnimal(Object.Instantiate(animal,gameObject.transform));
        }
    }
}