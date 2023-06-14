using System;
using _GameCore_.Common.StateMachine;
using UnityEngine;

namespace Core.DataStorage
{
    public class DataStorageSystems : Feature
    {
        public DataStorageSystems(Contexts contexts)
        {
            Add(new CheckAnimalAvailableSystem(contexts.data));
        }
    }
}