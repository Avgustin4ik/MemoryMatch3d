using System;
using Animals;

namespace Configs
{
    [Serializable]
    public struct SceneElement
    {
        public string SceneName;
        public AnimalsType AnimalType;
    }
}