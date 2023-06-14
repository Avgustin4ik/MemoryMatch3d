using Core.DataStorage;
using Core.UsedData;
using Entitas;

namespace Core
{
    public sealed class GameLevelSystems : Systems
    {
        public GameLevelSystems(Contexts contexts)
        {
            var gameConfig = Configs.ConfigsCatalogsManager.GetConfig<GameConfig>();
            
            Add(new DeveloperMode.EditorInputSystem(contexts));

            #region Core systems
            
            Add(new Input.InputGroupSystems(contexts));
            //
            Add(new UserDataSystems(contexts));
            //
            // Add(new ApplicationStates.ApplicationStateTransitionSystems(contexts.game, contexts.level, contexts.state));
            //
            Add(new GameLevels.GameLevelsSystems(contexts));

            #endregion

            // #region Services
            //
            // #endregion
            
            Add(new GameSystems(contexts, gameConfig)); // <<< add all you game system inside this systems

            // #region Event and cleanup systems (comment/uncomment to on/off each system)
            //
            
            Add(new GameEventSystems(contexts));
            Add(new GameCleanupSystems(contexts));
            
            Add(new InputCleanupSystems(contexts));
            Add(new InputEventSystems(contexts));
            Add(new ComplicatorsCleanupSystems(contexts));
            //
            // Add(new StateEventSystems(contexts));
            // Add(new StateCleanupSystems(contexts));
            //
            Add(new UiEventSystems(contexts));
            Add(new UiCleanupSystems(contexts));

            Add(new ComplicatorsCleanupSystems(contexts));
            //
            // #endregion
        }
    }
}