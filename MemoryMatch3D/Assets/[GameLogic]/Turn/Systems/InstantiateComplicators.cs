using System;
using System.Collections.Generic;
using System.Linq;
using Complicators;
using Entitas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Turn
{
    public class InstantiateComplicators : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _cellGroup;

        public InstantiateComplicators(GameContext contextsGame, LevelContext contextsLevel, ComplicatorsContext contextsComplicators) : base(contextsGame)
        {
            _cellGroup = contextsGame.GetGroup(GameMatcher.Cell);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.LoadingLevel.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            //todo need upgrade
            var blueprintEntity = Contexts.sharedInstance.level.GetEntitiesWithBlueprint(Contexts.sharedInstance.level.levelLoaderEntity.currentLevelNumber.value).First();
            var complicatorsSchemaOnLevel = blueprintEntity.complicatorsSchema.value;
            if(complicatorsSchemaOnLevel == null) return;
            foreach (var line in complicatorsSchemaOnLevel)
            {
                if(line.Value == Array.Empty<ComplicatorData>()) continue;
                var complicators = line.Value;
                for (int i = 0; i < complicators.Length; i++)
                {
                    if (complicators[i] == null)
                    {
                        continue;
                    }
                    var complicatorData = complicators[i];
                    var complicatorEntity =  Contexts.sharedInstance.complicators.CreateEntity();
                    complicatorEntity.isComplicator = true;
                    complicatorEntity.AddTurnNumber(line.Key);
                    complicatorEntity.AddPriority(i);
                    if(complicatorData.IsRepeatable) complicatorEntity.AddRepeatFrequency(complicatorData.RepeatFrequency);
                    complicatorEntity.isRandomIndexNeeded = complicatorData.RandomizeTarget;
                    switch (complicatorData.Type())
                    {
                        case ComplicatorType.DEFAULT:
                            break;
                        case ComplicatorType.SHIFT_COLUMN:
                            complicatorEntity.AddShiftColumn(((ShiftColumnComplicator)complicatorData).IsDirectionDefault);
                            complicatorEntity.AddIndex(((ShiftColumnComplicator)complicatorData).TargetColumnIndex);
                            break;
                        case ComplicatorType.SHIFT_ROW:
                            complicatorEntity.AddShiftRow(((ShiftRowComplicator)complicatorData).IsDirectionDefault);
                            complicatorEntity.AddIndex(((ShiftRowComplicator)complicatorData).TargetRowIndex);
                            break;
                        case ComplicatorType.SWITCH_TWO_POKEBALLS:
                            complicatorEntity.AddSwitchPokeballs(new Vector2Int[]
                            {
                                ((SwitchPokeballsComplicator)complicatorData).TargetACellIndex,
                                ((SwitchPokeballsComplicator)complicatorData).TargetBCellIndex,
                            });
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            Debug.Log("Complicators instantiation");
        }
        private int GetRandomIndex(bool forColumn)
        {
            var curLevel = Contexts.sharedInstance.level.levelLoaderEntity.currentLevelNumber.value;
            var gridSizeValue = Contexts.sharedInstance.level.GetEntitiesWithBlueprint(curLevel).First().gridSize.value;
            var max = forColumn ? gridSizeValue.x : gridSizeValue.y;
            var cells = _cellGroup.GetEntities().Where(cell => cell.hasLinkedPokeball);
            var index = 0;
            //todo здесь ошибка
            do
            { 
                index = Random.Range(0, max);
            } while (_cellGroup.GetEntities().Where(cell => forColumn ? cell.index.value.x == index : cell.index.value.y == index)
                     .All(cell => !cell.hasLinkedPokeball));
            return index;
        }
    }
}