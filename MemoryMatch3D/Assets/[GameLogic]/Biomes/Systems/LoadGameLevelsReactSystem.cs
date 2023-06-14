using System.Collections.Generic;
using Core;
using Entitas;

namespace Biomes
{
    public class LoadGameLevelsReactSystem : ReactiveSystem<InputEntity>
    {
        private readonly DataContext _dataContext;
        private readonly GameContext _gameContext;
        private readonly InputContext _inputContext;
        private readonly IGroup<DataEntity> _animalsDataGroup;
        private readonly IGroup<GameEntity> _selectedBiomeZoneGroup;

        public LoadGameLevelsReactSystem(InputContext contextsInput, GameContext contextsGame, DataContext contextsData
        ) : base(contextsInput)
        {
            _inputContext = contextsInput;
            _gameContext = contextsGame;
            _dataContext = contextsData;
            _animalsDataGroup = contextsData.GetGroup(DataMatcher.AnimalType);
            _selectedBiomeZoneGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.BiomeZone,
                GameMatcher.BiomeHub,
                GameMatcher.Selected));
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.ButtonLoadGameLevel);
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var selectedBiomeEntity = _selectedBiomeZoneGroup.GetSingleEntity();
            //todo: get data from selected biome
            var animalType = selectedBiomeEntity.biomeZone.value;
            var isLocked = selectedBiomeEntity.isLocked;
            //todo: сделать загрузку уровней по типу животного
            _dataContext.sceneLoaderEntity.ReplaceAnimalType(animalType);
            var sceneLoader = SceneLoader.Instance;
            // sceneLoader.LoadScene(sceneLoader.GameScene);
            sceneLoader.MMLoadGameLevel(sceneLoader.GameScene);
        }
    }
}