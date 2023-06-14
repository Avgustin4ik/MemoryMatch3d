using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Complicators
{
    public class CalculateRandomTargetForComplicatorSystem : ReactiveSystem<ComplicatorsEntity>
    {
        private readonly IGroup<GameEntity> _cellGroup;

        public CalculateRandomTargetForComplicatorSystem(GameContext contextsGame, ComplicatorsContext contextsComplicators) : base(contextsComplicators)
        {
            _cellGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.Cell));
        }

        protected override ICollector<ComplicatorsEntity> GetTrigger(IContext<ComplicatorsEntity> context)
        {
            return context.CreateCollector(ComplicatorsMatcher.ImplementTrigger.Added());
        }

        protected override bool Filter(ComplicatorsEntity entity)
        {
            return entity.isRandomIndexNeeded
                && _cellGroup.GetEntities().All(e => e.hasLinkedPokeball);
        }

        protected override void Execute(List<ComplicatorsEntity> entities)
        {
            var curLevel = Contexts.sharedInstance.level.levelLoaderEntity.currentLevelNumber.value;
            var gridSizeValue = Contexts.sharedInstance.level.GetEntitiesWithBlueprint(curLevel).First().gridSize.value;
            foreach (var complicatorsEntity in entities)
            {
                if (complicatorsEntity.hasSwitchPokeballs)
                    throw new NotImplementedException("TODO implement random positions switch");
                var forColumn = complicatorsEntity.hasShiftColumn;
                var max = forColumn ? gridSizeValue.x : gridSizeValue.y;
                var index = UnityEngine.Random.Range(0, max + 1);
                var cellEntities = _cellGroup.GetEntities();
                while (cellEntities.Where(cell => forColumn
                           ? cell.index.value.x == index
                           : cell.index.value.y == index)
                       .All(cell => !cell.hasLinkedPokeball))
                {
                    index  = UnityEngine.Random.Range(0, max + 1);
                }
                complicatorsEntity.ReplaceIndex(index);
                complicatorsEntity.isRandomIndexNeeded = false;
            }
            // var cells = _cellGroup.GetEntities().Where(cell => cell.hasLinkedPokeball);
            // var index = 0;
            // //todo здесь ошибка
            // do
            // { 
            //     index = Random.Range(0, max);
            // } while (_cellGroup.GetEntities().Where(cell => forColumn ? cell.index.value.x == index : cell.index.value.y == index)
            //          .All(cell => !cell.hasLinkedPokeball));
            // return index;
        }
    }
}