using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Pokeball
{
    public class DetectBorderPokeballSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _pokeballGroup;
        private readonly IGroup<GameEntity> _cellGroup;
        private readonly IGroup<LevelEntity> _levelGroup;
        private readonly GameContext _gameContext;

        public DetectBorderPokeballSystem(GameContext contextsGame) : base(contextsGame)
        {
            _gameContext = contextsGame;
            // _pokeballGroup = contextsGame.GetGroup(GameMatcher.Pokeball);
            _cellGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.Cell,
                GameMatcher.LinkedPokeball));
            _levelGroup = Contexts.sharedInstance.level.GetGroup(LevelMatcher.GridSize);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) => 
            context.CreateCollector(GameMatcher.AnyOf(
                GameMatcher.Pokeball,
                GameMatcher.LinkedPokeball));
        

        protected override bool Filter(GameEntity entity) => true;

        protected override void Execute(List<GameEntity> entities)
        {
            Debug.LogWarning("Detect border system triggered");
            var gridSize = _levelGroup.GetEntities().First().gridSize.value - Vector2Int.one;
            foreach (var cell in _cellGroup.GetEntities())
            {
                var borderList = new List<Vector2Int>();
                if(cell.hasLinkedPokeball == false) continue;
                if(cell.index.value.x == 0) borderList.Add(Vector2Int.left);
                if(cell.index.value.x == gridSize.x) borderList.Add(Vector2Int.right);
                if(cell.index.value.y == 0) borderList.Add(Vector2Int.up);
                if(cell.index.value.y == gridSize.y) borderList.Add(Vector2Int.down);
                //todo здесь ошибка
                var pokeballEntity = _gameContext.GetEntityWithHashCode(cell.linkedPokeball.value);
                if(borderList.Count == 0 && pokeballEntity.hasBorderNearby)
                {
                    pokeballEntity.RemoveBorderNearby();
                    continue;
                }
                pokeballEntity.ReplaceBorderNearby(borderList.ToArray());
            }
        }
    }
}