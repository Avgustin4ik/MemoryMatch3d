using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Tools;
using UnityEngine;

namespace Complicators
{
    public class SwitchPokeballsSystem : ReactiveSystem<ComplicatorsEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _cellGroup;

        public SwitchPokeballsSystem(GameContext contextsGame, ComplicatorsContext contextsComplicators) : base(contextsComplicators)
        {
            _gameContext = contextsGame;
            _cellGroup = _gameContext.GetGroup(GameMatcher.Cell);
        }

        protected override ICollector<ComplicatorsEntity> GetTrigger(IContext<ComplicatorsEntity> context) => 
            context.CreateCollector(ComplicatorsMatcher.ImplementTrigger.Added());

        protected override bool Filter(ComplicatorsEntity entity) =>
            entity.isComplicator
            && entity.hasSwitchPokeballs;

        protected override void Execute(List<ComplicatorsEntity> entities)
        {
            if (entities.Count != 1) throw new IndexOutOfRangeException("To mach complicators entity");
            var complicator = entities.FirstOrDefault();
            if (complicator.isRandomIndexNeeded) GetRandomTargets();
            
            var cells = _cellGroup.GetEntities()
                .Where(cell => complicator.switchPokeballs.value.Contains(cell.index.value)
                               && cell.hasLinkedPokeball)
                .ToArray();
            if (cells.Count() > 2) throw new NotImplementedException("Too match pokebalss in complicators query");
            if (cells.Count() < 2) GetRandomTargets();
            var pokeballs = cells.Select(c => _gameContext.GetEntityWithHashCode(c.linkedPokeball.value)).ToArray();
            for (int i = 0; i < cells.Count(); i++)
            {
                var pokeball = _gameContext.GetEntityWithHashCode(cells[i].linkedPokeball.value);
                cells[i].RemoveLinkedPokeball();
                var targetIndex = i + 1 == cells.Count() ? 0 : i + 1;
                pokeball.ReplaceSwitchToTargetCell(cells[targetIndex].hashCode.value,i == 0);
            }

            void GetRandomTargets()
            {
                if (complicator.hasSwitchPokeballs)
                {
                    if (_cellGroup.GetEntities().Count(cell => cell.hasLinkedPokeball) < 2)
                        throw new IndexOutOfRangeException("not enough pokeballs");
                    var rnd = new System.Random();
                    //todo only for 2 pokeballs
                    var cells = _cellGroup.GetEntities()
                        .Where(cell => cell.hasLinkedPokeball)
                        .OrderBy(c => rnd.Next())
                        .Take(2);
                    complicator.ReplaceSwitchPokeballs(new Vector2Int[]
                    {
                        cells.First().index.value, 
                        cells.Last().index.value
                    });
                    return; 
                }
            }
        }
    }
}