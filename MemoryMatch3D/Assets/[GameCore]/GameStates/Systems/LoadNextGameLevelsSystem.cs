using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class LoadNextGameLevelsSystem : ReactiveSystem<InputEntity>
    {
        public LoadNextGameLevelsSystem(InputContext contextsInput, GameContext contextsGame, DataContext contextsData) : base(contextsInput)
        {
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.ButtonLoadNextAnimalGameLevels);
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            throw new System.NotImplementedException("Load next game levels system");
        }
    }
}