using System.Collections.Generic;
using System.Linq;
using Entitas;
using Ui.Elements;
using UnityEngine;

namespace Boosters
{
    public class ExecuteBoosterOpenAllForceSystem : ReactiveSystem<GameEntity>
    {
        private readonly UiContext _uiContext;
        private readonly LevelContext _levelContext;

        public ExecuteBoosterOpenAllForceSystem(UiContext contextsUI, GameContext contextsGame): base(contextsGame)
        {
            _uiContext = contextsUI;
            _levelContext = Contexts.sharedInstance.level;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.PreGame.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager
                   && entity.isBoosterUsedFlag == false;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var condition = _levelContext.GetEntitiesWithBlueprint(_levelContext.levelLoaderEntity.currentLevelNumber.value).First()
                .isFreePreGameBooster == false;
            if(condition) return;
            foreach (var stateManager in entities)
            {
                Debug.LogWarning($"Booster-OpenAll Force Execution");
                var requestEntity = _uiContext.CreateEntity();
                requestEntity.isBoosterImplementationRequest = true;
                requestEntity.AddBoosterID(999);
                requestEntity.AddBoosterType(typeof(BoosterOpenAll));
            }
        }
    }
}