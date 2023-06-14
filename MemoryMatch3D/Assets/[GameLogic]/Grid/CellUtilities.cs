using System;
using System.Linq;
using UnityEngine;

namespace Grid
{
    public static class CellUtilities
    {
        public static bool TryGetLinkedPokeball(this GameEntity cellEntity, out GameEntity pokeballEntity)
        {
            pokeballEntity = null;
            if(!cellEntity.hasLinkedPokeball) return false;
            var pokeballHash = cellEntity.linkedPokeball.value;
            pokeballEntity = Contexts.sharedInstance.game.GetEntityWithHashCode(pokeballHash);
            // var entities = Contexts.sharedInstance.game.GetGroup(GameMatcher.Pokeball)
            //     .GetEntities()
            //     .Where(e => e.hashCode.value == pokeballHash);
            // if (entities.Count() > 1)
            //     throw new ArgumentOutOfRangeException($"to mach pokeballs with hashcode {pokeballHash}");
            // pokeballEntity = entities.FirstOrDefault();
            return true;
        }
        /// <summary>
        /// Try to get closest cell in one from four hard directions (up, down, left, right)
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="direction"> Vector2.up, Vector2.down, Vector2.left or Vector2.right </param>
        /// <param name="neighborCell"></param>
        /// <returns></returns>
        public static Vector2Int ConvertDirection(Vector2Int visualDirection) => visualDirection.y != 0 ? new Vector2Int(visualDirection.x, -visualDirection.y) : visualDirection;
        public static bool TryGetNeighborCell(this GameEntity cell, Vector2Int direction,  out GameEntity neighborCell)
        {
            neighborCell = null;
            if (CheckDirection(direction) == false)
                throw new ArgumentOutOfRangeException("Only hard straight vectors. Up, Down, Left, Right");
            var contexts = Contexts.sharedInstance;
            var gridSize = contexts.level
                .GetEntitiesWithBlueprint(contexts.level.levelLoaderEntity.currentLevelNumber.value)
                .First()
                .gridSize.value - Vector2Int.one;
            var cellIndex = cell.index.value;
            var entitiesWithIndex = contexts.game.GetEntitiesWithIndex(cellIndex + ConvertDirection(direction));
            if (!entitiesWithIndex.Any())
            {
                if(direction == Vector2Int.left) neighborCell = contexts.game.GetEntitiesWithIndex(new Vector2Int(gridSize.x,cellIndex.y)).First();
                if(direction == Vector2Int.right) neighborCell = contexts.game.GetEntitiesWithIndex(new Vector2Int(0,cellIndex.y)).First();
                if(direction == Vector2Int.down) neighborCell = contexts.game.GetEntitiesWithIndex(new Vector2Int(cellIndex.x,0)).First();
                if(direction == Vector2Int.up) neighborCell = contexts.game.GetEntitiesWithIndex(new Vector2Int(cellIndex.x,gridSize.y)).First();
                return false;
            }
            neighborCell = entitiesWithIndex.First();
            return neighborCell != null;
        }


        private static bool CheckDirection(Vector2Int direction)
        {
            return direction == Vector2Int.down ||
                   direction == Vector2Int.left ||
                   direction == Vector2Int.up ||
                   direction == Vector2Int.right;
        }
    }
}