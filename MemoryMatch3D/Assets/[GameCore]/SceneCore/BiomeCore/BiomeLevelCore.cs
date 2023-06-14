using Core.Configs;
using Entitas;

namespace Core
{
    public class BiomeLevelCore : ECSSceneCore
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
            _gameSystems = new BiomeLevelSystems(Contexts.sharedInstance);
        }
    }
}