using System;
using System.Collections.Generic;
using Entitas;

namespace _GameCore_.Common.StateMachine
{
    public class StateMachineSystems : Feature
    {
        public StateMachineSystems(Contexts contexts)
        {
            Add(new StateTransitionSystem(contexts));
        }
    }

    public class StateTransitionSystem : ReactiveSystem<GameEntity>
    {
        public StateTransitionSystem(Contexts contexts) : base(context:contexts.game)
        {
            
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.StateTransition.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                //todo implement state replacement logic
                
                //end logic
                entity.RemoveChangeStateRequest();
            }
            
        }
    }
}