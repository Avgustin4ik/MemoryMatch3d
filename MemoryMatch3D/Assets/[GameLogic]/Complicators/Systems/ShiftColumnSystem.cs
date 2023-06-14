using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Grid;
using Tools;
using UnityEngine;

namespace Complicators
{
    public class ShiftColumnSystem : ReactiveSystem<ComplicatorsEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _cellGroup;

        public ShiftColumnSystem(GameContext contextsGame, ComplicatorsContext contextsComplicators) : base(contextsComplicators)
        {
            _gameContext = contextsGame;
            _cellGroup = _gameContext.GetGroup(GameMatcher.Cell);
        }

        protected override ICollector<ComplicatorsEntity> GetTrigger(IContext<ComplicatorsEntity> context) =>
            context.CreateCollector(ComplicatorsMatcher.ImplementTrigger.Added());

        protected override bool Filter(ComplicatorsEntity entity) =>
            entity.isComplicator
            && entity.hasShiftColumn;

        protected override void Execute(List<ComplicatorsEntity> entities)
        {
            if (entities.Count != 1) throw new IndexOutOfRangeException("To mach complicators entity");
            var complicator = entities.FirstOrDefault();
            var cells = _cellGroup.GetEntities()
                .Where(cell => cell.index.value.x == complicator.index.value);
            var isDirectionUpToDown = complicator.shiftColumn.upToDown;
            complicator.CheckIndex();
            foreach (var cell in cells)
            {
                if (cell.TryGetLinkedPokeball(out var pokeballEntity) == false) continue;
                cell.RemoveLinkedPokeball();
                var direction = isDirectionUpToDown ? Vector2Int.down : Vector2Int.up;
                var newBreakBorderDirection = Vector2Int.zero;
                if (cell.TryGetNeighborCell(direction, out var targetCell) == false)
                    newBreakBorderDirection = direction;

                pokeballEntity.ReplaceMoveToTargetCell(targetCell.hashCode.value, newBreakBorderDirection);
            }
        }
    }
}