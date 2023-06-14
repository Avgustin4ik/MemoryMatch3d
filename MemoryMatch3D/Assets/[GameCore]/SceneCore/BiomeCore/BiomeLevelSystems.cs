using Biomes;
using Core.DataStorage;
using Core.Input;
using Entitas;
using Feedbacks;
using GameCamera.Systems;

namespace Core
{
    public class BiomeLevelSystems : Feature
    {
        public BiomeLevelSystems(Contexts contexts)
        {
            Add(new InputGroupSystems(contexts));
            Add(new DataStorageSystems(contexts));
            Add(new VirtualCameraSystems(contexts));
            Add(new BiomeSystems(contexts));
            Add(new Ui.UISystems(contexts.game, contexts.state, contexts.ui));
            Add(new FeedbacksSystems(contexts));
            //
            Add(new GameCleanupSystems(contexts));
            Add(new GameEventSystems(contexts));
            Add(new InputCleanupSystems(contexts));
            Add(new InputEventSystems(contexts));
            Add(new UiCleanupSystems(contexts));
            Add(new UiEventSystems(contexts));
            Add(new DataEventSystems(contexts));
            Add(new DataCleanupSystems(contexts));
        }
    }
}