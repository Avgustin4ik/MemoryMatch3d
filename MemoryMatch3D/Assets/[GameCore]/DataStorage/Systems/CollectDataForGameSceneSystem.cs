using System.Collections.Generic;
using Entitas;

namespace Core.DataStorage
{
    public class CollectDataForGameSceneSystem : ReactiveSystem<InputEntity>
    {
        private readonly DataContext _dataContext;
        private readonly InputContext _inputContext;
        private readonly DataEntity _sceneLoader;

        public CollectDataForGameSceneSystem(InputContext contextsInput, DataContext contextsData) : base(contextsInput)
        {
            _inputContext = contextsInput;
            _dataContext = contextsData;
            _sceneLoader = _dataContext.sceneLoaderEntity;
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
            // _sceneLoader.AddAnimalType();
        }
    }
}