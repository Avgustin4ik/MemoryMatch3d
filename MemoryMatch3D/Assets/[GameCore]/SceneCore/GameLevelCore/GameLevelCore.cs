using Core.Configs;
using Entitas;
using GameCamera.Systems;
using Pokeball;

namespace Core
{
    public class GameLevelCore : ECSSceneCore
    {
        
        protected override void Awake()
        {
            base.Awake();
            ConfigsCatalogsManager.LoadCatalogs();
            
            _doNotDestroyOnLoadContexts = new IContext[]
            {
                Contexts.sharedInstance.data,
                Contexts.sharedInstance.level
            };
            _gameSystems = new GameLevelSystems(Contexts.sharedInstance);
        }
    }
}