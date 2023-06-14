using Core.Configs;
using Entitas;
using UnityEngine;

namespace Core
{
    public sealed class GameCore : MonoBehaviour
    {
        private Contexts _contexts;
        private Systems _gameCoreSystems;

        private void Awake()
        {
            ConfigsCatalogsManager.LoadCatalogs();

            _contexts = Contexts.sharedInstance;
            _gameCoreSystems = new GameLevelSystems(_contexts);
        }

        private void Start()
        {
            _gameCoreSystems.Initialize();
        }

        private void Update()
        {
            _gameCoreSystems.Execute();
            _gameCoreSystems.Cleanup();
        }

        private void OnDestroy()
        {
            _gameCoreSystems.TearDown();     
            _gameCoreSystems.DeactivateReactiveSystems();
            _gameCoreSystems.ClearReactiveSystems();
            foreach (var sharedInstanceAllContext in Contexts.sharedInstance.allContexts)
            {
                if(sharedInstanceAllContext == Contexts.sharedInstance.data) continue;
                // sharedInstanceAllContext.DestroyAllEntities();
                sharedInstanceAllContext.Reset();
            }
        }
    }
}