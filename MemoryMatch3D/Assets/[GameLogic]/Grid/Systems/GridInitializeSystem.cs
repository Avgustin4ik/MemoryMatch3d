using System;
using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Entitas;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Grid
{
    public class GridInitializeSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _gridGroup;
        private readonly LevelContext _levelContext;

        public GridInitializeSystem(GameContext contextsGame) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _gridGroup = _gameContext.GetGroup(GameMatcher.Grid);
            _levelContext = Contexts.sharedInstance.level;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Level.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var levelEntity in entities)
            {
                foreach (var gridEntity in _gridGroup.GetEntities())
                {
                    var gameConfig = ConfigsCatalogsManager.GetConfig<GameConfig>();
                    var cellPrefab = gameConfig.CellPrefab;
                    //initialization logic
                    if( !cellPrefab.TryGetComponent<MeshFilter>(out var meshFilter)) continue;
                    var cellSize = Tools.EntitasTools.CalculateAABB(meshFilter.sharedMesh);
                    cellSize = Vector3.Scale(cellSize , cellPrefab.transform.localScale);
                    var currentLevel = _levelContext.levelLoaderEntity.currentLevelNumber.value;
                    var entitiesWithBlueprint = _levelContext.GetEntitiesWithBlueprint(currentLevel).FirstOrDefault();
                    if (entitiesWithBlueprint == null) throw new ArgumentNullException("No levelBlueprint was find");
                    var wight = entitiesWithBlueprint.gridSize.value.x;
                    var height = entitiesWithBlueprint.gridSize.value.y;
                    
                    var zeroPos = gridEntity.transform.value.position;
                    if (wight % 2 == 0) zeroPos += Vector3.right * (cellSize.x)/2f ;
                    if (height % 2 == 0) zeroPos += Vector3.forward * (cellSize.z) / 2f;

                    Vector2 clearance = gridEntity.clearance.value;
                    var startPos = new Vector3(
                        zeroPos.x - wight / 2 * (cellSize.x+clearance.x),
                        zeroPos.y,
                        zeroPos.z - height / 2 * (cellSize.z + clearance.y));

                    for (int i = 0; i < wight; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            var offset = new Vector3(
                                (cellSize.x + clearance.x)*i,
                                zeroPos.y,
                                (cellSize.z + clearance.y)*j);
                            
                            var go = Object.Instantiate<CellView>(cellPrefab, startPos + offset, quaternion.identity);
                            var cellEntity = _gameContext.CreateEntity();
                            cellEntity.AddIndex(new Vector2Int(i,height-1- j));
                            go.Init(cellEntity);
                        }
                    }
                }
            }

            _gameContext.CreateEntity().eventGridComplete = true;
        }
    }
}