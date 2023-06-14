using Entitas;
using UnityEngine;

namespace Core
{
    public class GlobalCheatcodeSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly DataContext _dataContext;


        public GlobalCheatcodeSystem(Contexts contexts)
        {
            _gameContext = contexts.game;
            _dataContext = contexts.data;
        }

        public void Execute()
        {
            if (UnityEngine.Input.GetKey(KeyCode.P) && UnityEngine.Input.GetKeyDown(KeyCode.Alpha0))
            {
                _dataContext.CreateEntity().isRequestDeleteSaveData = true;
                UnityEngine.Debug.LogWarning("All save was deleted");
            }
        }
    }
}