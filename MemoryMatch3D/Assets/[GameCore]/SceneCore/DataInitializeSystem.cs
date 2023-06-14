using Core.DataStorage;
using Grid;

namespace Core
{
    public class DataInitializeSystem : Feature
    {
        public DataInitializeSystem(Contexts contexts)
        {
            Add(new InstantiateSceneLoaderSystem(contexts.data));
            Add(new ReadLevelsBlueprintsInitializationSystem(contexts.level));
            Add(new LoadAvailableAnimalsSystem(contexts.data, contexts.level));
            Add(new SaveAnimalUnlockProgressSystem(contexts.data));
        }
    }
}