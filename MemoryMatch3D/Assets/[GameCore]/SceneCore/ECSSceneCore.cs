using System.Linq;
using Entitas;
using Entitas.VisualDebugging.Unity;
using MoreMountains.Tools;
using UnityEngine;

namespace Core
{
    public class ECSSceneCore : MonoBehaviour
    {
        protected Systems _gameSystems;
        protected IContext[] _doNotDestroyOnLoadContexts;
        private Contexts _contexts;


        protected virtual void Awake()
        {
            _contexts = Contexts.sharedInstance;
            
        }
        
        private void Start()
        {
            _gameSystems.Initialize();
        }
        
        private void Update()
        {
            _gameSystems.Execute();
            _gameSystems.Cleanup();
        }
        
        private void OnDestroy()
        {
            _gameSystems.TearDown();
            _gameSystems.DeactivateReactiveSystems();
            _gameSystems.ClearReactiveSystems();
            var contextsToClean = _contexts.allContexts.Where(x => !_doNotDestroyOnLoadContexts.Contains(x));
            foreach (var context in contextsToClean)
            {
                context.Reset();
            }

        }
    }
}