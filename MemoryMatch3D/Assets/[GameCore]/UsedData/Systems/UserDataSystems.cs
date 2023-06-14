namespace Core.UsedData
{
    public class UserDataSystems : Feature
    {
        public UserDataSystems(Contexts contexts)
        {
            Add(new UserDataInitializeSystem(contexts.game));
        }
    }
}
