using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Tools
{
    public static class EntitasTools
    {

        public static Vector3 CalculateAABB(Mesh mesh)
        {
            var vector3S = mesh.vertices.OrderBy(v => v.x).ThenBy(v => v.y).ThenBy(v => v.z);
            return vector3S.Last() - vector3S.First();
        }
        

        public static void CheckIndex(this ComplicatorsEntity complicator)
        {
            var cellGroup = Contexts.sharedInstance.game.GetGroup(GameMatcher.Cell);
            var index = complicator.index.value;

            bool forColumn = complicator.hasShiftColumn;
            Func<GameEntity,bool> predicate = cell => forColumn ? cell.index.value.x == index : cell.index.value.y == index;
            bool IsCellsAreEmpty() => cellGroup.GetEntities().Where(predicate)
                .All(cell => !cell.hasLinkedPokeball);
            if (index >= 0 && !complicator.isRandomIndexNeeded && !IsCellsAreEmpty()) return; //old index passed
#if UNITY_EDITOR
            Debug.LogWarning($"Complicators need new index");
#endif
            var curLevel = Contexts.sharedInstance.level.levelLoaderEntity.currentLevelNumber.value;
            var gridSizeValue = Contexts.sharedInstance.level.GetEntitiesWithBlueprint(curLevel).First().gridSize.value;
            var max = forColumn ? gridSizeValue.x : gridSizeValue.y;
            var rnd = new System.Random();
            var cellsWithPokeballs = cellGroup.GetEntities().Where(cell => cell.hasLinkedPokeball);
            if (!cellsWithPokeballs.Any())
            {
                Debug.LogWarning("No linked pokeballs was found");
                return;
                // throw new Exception("No linked pokeballs was found");
            }
            var cell = cellsWithPokeballs.OrderBy(cell => rnd.Next()).First();
            
            complicator.ReplaceIndex(forColumn ? cell.index.value.x : cell.index.value.y);
        }
        
#if UNITY_EDITOR
        [MenuItem("Tools/Delete All Saves")]
        public static void DeleteAllSaves()
        {
            PlayerPrefs.DeleteAll();
            Debug.LogWarning("All save data was deleted)");
        }
#endif
        public static System.Linq.IOrderedEnumerable<TSource> Shuffle<TSource>(
            this System.Collections.Generic.IEnumerable<TSource> source)
        {
            var rnd = new System.Random();
            return source.OrderBy(x => rnd.Next());
        }

    }
}
