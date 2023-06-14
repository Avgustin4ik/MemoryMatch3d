using Biomes;
using Core.DataStorage;
using Core.GameStates;
using Entitas;
using Feedbacks;
using GameCamera.Systems;
using GameStates.Systems;
using Player;
using Pokeball;
using Turn;

public sealed class GameSystems : Systems
{
    public GameSystems(Contexts contexts, GameConfig gameConfig)
    {
        // add you game logic here
        // Add(new GameLoadPlayerDataCompleteReactSystem(contexts.game, contexts.level));
        
        Add(new StateSystems(contexts));
        Add(new GameCameraSystems(contexts));

        Add(new DataStorageSystems(contexts));
        Add(new PlayerSystems(contexts));
        Add(new TurnSystems(contexts));
        Add(new Grid.GridSystems(contexts));
        Add(new GameStateSystems(contexts));
        Add(new PokeballsSytems(contexts));
        Add(new Boosters.BoosterSystems(contexts));
        Add(new Complicators.ComplicatorsSystems(contexts));

        // ui logic here
        Add(new Ui.UISystems(contexts.game, contexts.state, contexts.ui));
        
        Add(new FeedbacksSystems(contexts));
        // Debug
        Add(new DebugMenu.DebugMenuSystems(contexts));
    }
}

