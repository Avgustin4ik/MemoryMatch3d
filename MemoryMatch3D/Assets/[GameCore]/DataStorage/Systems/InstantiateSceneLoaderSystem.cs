using Entitas;

namespace Core.DataStorage
{
    public class InstantiateSceneLoaderSystem : IInitializeSystem
    {
        public InstantiateSceneLoaderSystem(DataContext contextsData)
        {
        }

        public void Initialize()
        {
            Contexts.sharedInstance.data.isSceneLoader = true;   
        }
    }
}