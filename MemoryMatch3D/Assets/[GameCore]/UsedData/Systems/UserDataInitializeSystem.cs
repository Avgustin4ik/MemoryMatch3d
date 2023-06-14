using System.Collections.Generic;
using Entitas;

namespace Core.UsedData
{
    public class UserDataInitializeSystem : IInitializeSystem
    {
        private readonly GameContext _gameContext;

        public UserDataInitializeSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Initialize()
        {
            var userData = _gameContext.CreateEntity();
            userData.isUserData = true;
            userData.AddUserDataMoney(default);
            userData.AddCurrentLevel(default);
            userData.AddOpenedLevel(default);
        }
    }
}