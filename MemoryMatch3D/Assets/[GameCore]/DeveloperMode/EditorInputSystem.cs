using System.Linq;
using Boosters;
using Entitas;
using Turn;
using UnityEngine;

namespace Core.DeveloperMode
{
    /// <summary>
    /// Система ввода в режиме эдитора
    /// F+Z Применить рандомный зум к камере
    /// </summary>
    public class EditorInputSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;

        public EditorInputSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            if (UnityEngine.Input.GetKey(KeyCode.F) && UnityEngine.Input.GetKeyUp(KeyCode.Z))
            {
                Debug.LogWarning($"Cheat is active. F+Z Random zoom");
                Contexts.sharedInstance.game.CreateEntity()
                    .AddGameCameraZoomEvent(Random.Range(40f,90f));
                return;
            }
            if (UnityEngine.Input.GetKey(KeyCode.C) && UnityEngine.Input.GetKeyUp(KeyCode.Z))
            {
                Debug.LogWarning($"Cheat is active. F+Z Random zoom");
                Contexts.sharedInstance.game.CreateEntity()
                    .triggerCheckObjectsInCameraEvent = true;
                return;
            }

            if (UnityEngine.Input.GetKey(KeyCode.B)&&(UnityEngine.Input.GetKeyDown(KeyCode.A)))
            {
                Debug.Log("Booster aim mode");
                if (_contexts.game.stateManagerEntity.stateBoosterAiming)
                {
                    var group = _contexts.game.GetGroup(GameMatcher.Booster);
                    foreach (var e in group)
                    {
                        e.Destroy();
                    }
                    _contexts.game.stateManagerEntity.stateBoosterAiming = false;
                }
                else
                {
                    var boosterEntity = _contexts.game.CreateEntity();
                    boosterEntity.isBooster = true;
                    boosterEntity.isSelected = true;
                    boosterEntity.AddAimTarget(AimTarget.All);
                    _contexts.game.stateManagerEntity.stateBoosterAiming = true;
                }
            }

            if (UnityEngine.Input.GetKey(KeyCode.C) && (UnityEngine.Input.GetKeyDown(KeyCode.D)))
            {
                var index = Random.Range(0,
                    _contexts.level.GetGroup(LevelMatcher.GridSize).GetEntities().First().gridSize.value.x);
                Debug.LogWarning($"Shift down {index} column");
                var complicatorsEntity = _contexts.complicators.CreateEntity();
                complicatorsEntity.isComplicator = true;
                complicatorsEntity.AddPriority(0);
                complicatorsEntity.AddShiftColumn(true);
                complicatorsEntity.AddIndex(index);
                complicatorsEntity.isImplementTrigger = true;
            }
            if (UnityEngine.Input.GetKey(KeyCode.C) && (UnityEngine.Input.GetKeyDown(KeyCode.U)))
            {
                var index = Random.Range(0,
                    _contexts.level.GetGroup(LevelMatcher.GridSize).GetEntities().First().gridSize.value.x);
                Debug.LogWarning($"Shift up {index} column");
                var complicatorsEntity = _contexts.complicators.CreateEntity();
                complicatorsEntity.isComplicator = true;
                complicatorsEntity.AddPriority(0);
                complicatorsEntity.AddShiftColumn(false);
                complicatorsEntity.AddIndex(index);
                complicatorsEntity.isImplementTrigger = true;
            }
            if (UnityEngine.Input.GetKey(KeyCode.C) && (UnityEngine.Input.GetKeyDown(KeyCode.L)))
            {
                var index = Random.Range(0,
                    _contexts.level.GetGroup(LevelMatcher.GridSize).GetEntities().First().gridSize.value.x);
                Debug.LogWarning($"Shift left {index} column");
                var complicatorsEntity = _contexts.complicators.CreateEntity();
                complicatorsEntity.isComplicator = true;
                complicatorsEntity.AddPriority(0);
                complicatorsEntity.AddShiftRow(false);
                complicatorsEntity.AddIndex(index);
                complicatorsEntity.isImplementTrigger = true;
            }
            if (UnityEngine.Input.GetKey(KeyCode.C) && (UnityEngine.Input.GetKeyDown(KeyCode.R)))
            {
                var index = Random.Range(0,
                    _contexts.level.GetGroup(LevelMatcher.GridSize).GetEntities().First().gridSize.value.x);
                Debug.LogWarning($"Shift right {index} column");
                var complicatorsEntity = _contexts.complicators.CreateEntity();
                complicatorsEntity.isComplicator = true;
                complicatorsEntity.AddPriority(0);
                complicatorsEntity.AddShiftRow(true);
                complicatorsEntity.AddIndex(index);
                complicatorsEntity.isImplementTrigger = true;
            }

            if (UnityEngine.Input.GetKey(KeyCode.C) && UnityEngine.Input.GetKeyDown(KeyCode.S))
            {
                var rnd = new System.Random();
                var cells = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Cell,
                        GameMatcher.LinkedPokeball))
                    .GetEntities()
                    .OrderBy(x => rnd.Next()).ToArray();
                Debug.LogWarning($"Switch {cells[0].index.value} cell and {cells[1].index.value} cell");
                var complicatorsEntity = _contexts.complicators.CreateEntity();
                complicatorsEntity.AddPriority(0);
                complicatorsEntity.isComplicator = true;
                complicatorsEntity.AddSwitchPokeballs(new []
                {
                    cells[0].index.value,
                    cells[1].index.value
                });
                complicatorsEntity.isImplementTrigger = true;
            }
            
            if(UnityEngine.Input.GetKey(KeyCode.L) && UnityEngine.Input.GetKeyDown(KeyCode.W))
            {
                Debug.LogWarning($"Cheat is active. L+W Win level");
                _contexts.game.stateManagerEntity.stateVictory = true;
                var animalType = _contexts.data.sceneLoaderEntity.animalType.value;
                var group = _contexts.data.GetGroup(DataMatcher.AllOf(DataMatcher.AnimalType,DataMatcher.UnlockProgress));
                var animalDataEntity = group.GetEntities().FirstOrDefault(x => x.animalType.value == animalType);
                animalDataEntity?.ReplaceUnlockProgress(animalDataEntity.unlockGoalLimit.value);
            }
            
            

        }
    }
}