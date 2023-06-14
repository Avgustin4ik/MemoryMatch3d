using System;
using Core.UsedData;

namespace Core
{
    public class CoreSystems : Feature
    {
        public CoreSystems(Contexts contexts)
        {
#if UNITY_EDITOR
            Add(new GlobalCheatcodeSystem(contexts));
#endif
            Add(new DataInitializeSystem(contexts));
        }
    }
}