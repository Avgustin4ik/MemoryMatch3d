using Core.Configs;
using Core.Extension;
using Entitas;
using Grid;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.GameLevels
{
    public class LevelSchema : MonoBehAdvGameLevelCleanup
    {
        [FormerlySerializedAs("gridPivotPivot")] [SerializeField] private GridPivotView gridPivot;
        private CellView _cellPrefab;
        [SerializeField] private Vector2 clearance;
#if UNITY_EDITOR
        [Header("Gizmo")]
        [SerializeField] private int gridWidth;
        [SerializeField] private int gridHeight;
        [SerializeField] private Color color;
        [SerializeField] private Color wireColor;
#endif

        public string GameObjectName
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.isLevel = true;
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            var curLevel = Contexts.sharedInstance.game.userDataEntity.currentLevel.value;
            var gridEntity = Contexts.sharedInstance.game.CreateEntity();
            gridEntity.AddClearance(clearance);
            gridPivot.Init(gridEntity);
        }
        private Transform _gridTransform;
        
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            //todo Переделать в более легковесный вариант
            if(gridPivot== null ) return;
            _gridTransform ??= gridPivot.transform;
            ConfigsCatalogsManager.LoadCatalogs();
            _cellPrefab = ConfigsCatalogsManager.GetConfig<GameConfig>().CellPrefab;
            if( !_cellPrefab.TryGetComponent<MeshFilter>(out var meshFilter)) return;
            var cellSize = Tools.EntitasTools.CalculateAABB(meshFilter.sharedMesh);
            cellSize = Vector3.Scale(cellSize , _cellPrefab.transform.localScale);

            var zeroPos = gridPivot.transform.position;
            if (gridWidth % 2 == 0) zeroPos += Vector3.right * (cellSize.x)/2f ;
            if (gridHeight % 2 == 0) zeroPos += Vector3.forward * (cellSize.z) / 2f;
            
            var startPos = new Vector3(
                zeroPos.x - gridWidth / 2 * (cellSize.x+clearance.x),
                zeroPos.y,
                zeroPos.z - gridHeight / 2 * (cellSize.z + clearance.y));
            
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    var offset = new Vector3(
                        (cellSize.x+this.clearance.x)*i,
                        zeroPos.y,
                        (cellSize.z+this.clearance.y)*j);
                    Gizmos.color = wireColor;
                    Gizmos.DrawWireMesh(meshFilter.sharedMesh,startPos+offset,Quaternion.identity);
                    Gizmos.color = color;
                    Gizmos.DrawMesh(meshFilter.sharedMesh, startPos+offset,Quaternion.identity);
                }
            }
        }
#endif
    }
}