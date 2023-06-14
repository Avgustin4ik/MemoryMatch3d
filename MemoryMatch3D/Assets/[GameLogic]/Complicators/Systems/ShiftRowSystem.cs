using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Grid;
using Tools;
using UnityEngine;

namespace Complicators
{
    public class ShiftRowSystem : ReactiveSystem<ComplicatorsEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _cellGroup;
        private readonly IGroup<GameEntity> _pokeballGroup;
        public ShiftRowSystem(GameContext contextsGame, ComplicatorsContext contextsComplicators) : base(contextsComplicators)
        {
            _gameContext = contextsGame;
            _cellGroup = _gameContext.GetGroup(GameMatcher.Cell);
        }

        protected override ICollector<ComplicatorsEntity> GetTrigger(IContext<ComplicatorsEntity> context) => context.CreateCollector(ComplicatorsMatcher.ImplementTrigger.Added());

        protected override bool Filter(ComplicatorsEntity entity) =>
            entity.isComplicator
            && entity.hasShiftRow;

        protected override void Execute(List<ComplicatorsEntity> entities)
        {
            if (entities.Count != 1) throw new IndexOutOfRangeException("To mach complicators entity");
            var complicator = entities.FirstOrDefault();
            var cells = _cellGroup.GetEntities()
                .Where(cell => cell.index.value.y == complicator.index.value);
            var isDirectionLeftToRight = complicator.shiftRow.leftToRight; 
            complicator.CheckIndex();
            foreach (var cell in cells)
            {
                if (cell.TryGetLinkedPokeball(out var pokeballEntity) == false) continue;
                cell.RemoveLinkedPokeball();
                var direction = isDirectionLeftToRight ? Vector2Int.left : Vector2Int.right;
                var newBreakBorderDirection = Vector2Int.zero;
                if (cell.TryGetNeighborCell(direction, out var targetCell) == false)
                    newBreakBorderDirection = direction;

                pokeballEntity.ReplaceMoveToTargetCell(targetCell.hashCode.value, newBreakBorderDirection);
            }
            
        }
    }
}