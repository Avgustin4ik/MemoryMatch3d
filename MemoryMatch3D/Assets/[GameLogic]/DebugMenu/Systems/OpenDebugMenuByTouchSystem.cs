using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace DebugMenu
{
    public class OpenDebugMenuByTouchSystem : ReactiveSystem<InputEntity>
    {
        public OpenDebugMenuByTouchSystem(InputContext contextsInput, UiContext contextsUI) : base(contextsInput)
        {
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.TouchPhase);
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.isTouchData &&
                   entity.touchPhase.value.Equals(TouchPhase.Ended);
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var camera = Contexts.sharedInstance.game.GetGroup(GameMatcher.GameCamera).GetEntities().First().gameCameraMain.value;
            var debugX = 200f;
            var debugY = camera.pixelHeight - 200f;
            foreach (var touchDataEntity in entities)
            {
                var isInRange = touchDataEntity.touchDownPosition.value.x < debugX &&
                                touchDataEntity.touchDownPosition.value.y > debugY;
                if (isInRange == false) return;
                touchDataEntity.ReplaceLastTimeTouchDetected(Time.time);
                const float deltaTime = 0.7f;
                if(Time.time - touchDataEntity.lastTimeTouchDetected.value > deltaTime) return;
                int GetTouchCount() => touchDataEntity.touchForDebugCount.value;
                touchDataEntity.ReplaceTouchForDebugCount(GetTouchCount() + 1);
                if (GetTouchCount() < 3) continue;
                touchDataEntity.ReplaceTouchForDebugCount(0);
                Contexts.sharedInstance.game.stateManagerEntity.stateDebugMode = true;
            }
        }
    }
}