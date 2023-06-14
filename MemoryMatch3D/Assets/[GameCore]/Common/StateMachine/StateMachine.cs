using System;
using System.Linq;
using _GameCore_.Common.StateMachine.Components;
using Entitas;
using UnityEngine;

namespace _GameCore_.Common.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private void Awake()
        {
            Contexts.sharedInstance.game.isStateManager = true;
            Contexts.sharedInstance.game.stateManagerEntity.OnComponentAdded += OnOnComponentAdded;
            DontDestroyOnLoad(this);
        }

        private void OnOnComponentAdded(IEntity entity, int index, IComponent component)
        {
            if(component is IGameStateComponent == false) return;
            Debug.LogWarning($"State was changed! New State is {component.GetType().Name}");
            var gameStateManagerEntity = Contexts.sharedInstance.game.stateManagerEntity;
            var stateComponents = gameStateManagerEntity.GetComponents().Where(x => x is IGameStateComponent);
            stateComponents.FirstOrDefault(x => x.GetType() == typeof(Component));
            var componentIndices = entity.GetComponentIndices();
            foreach (var componentIndex in componentIndices)
            {
                var currentComponent = entity.GetComponent(componentIndex);
                if (currentComponent is not IGameStateComponent) continue;
                // if (currentComponent)
            }
        }

        private void OnDestroy()
        {
            Contexts.sharedInstance.game.stateManagerEntity.OnComponentAdded -= OnOnComponentAdded;
        }

        public static void ChangeState(IGameStateComponent newGameState)
        {
            Contexts.sharedInstance.game.stateManagerEntity.ReplaceCurrentState(newGameState);
        }
        
        

        private void Update()
        {
            
        }
    }
}