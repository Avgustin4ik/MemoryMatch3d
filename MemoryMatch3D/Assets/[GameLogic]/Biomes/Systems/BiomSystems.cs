namespace Biomes
{
    public class BiomeSystems : Feature
    {
        public BiomeSystems(Contexts contexts)
        {
            
            Add(new BiomeInitializeSystem(contexts.game, contexts.data));
            Add(new ReadBiomeData(contexts.game, contexts.data));
            Add(new LockAllAnimalsByInitSystem(contexts.game, contexts.data));
            // Add(new OpenNewAnimalReactSystem(contexts.data, contexts.game)); //todo visual logic
            // Add(new UnlockNextBiomeZoneSystem(contexts.game, contexts.data));
            Add(new SelectionBiomeZoneSystem(contexts.game));
            Add(new SelectFirstAvailableBiomeZone(contexts.game, contexts.data));
            
            Add(new ShowLevelScreenSystem(contexts.game, contexts.data, contexts.ui));
            Add(new SelectNextBiomeReactionSystem(contexts.input, contexts.game));
            Add(new LoadGameLevelsReactSystem(contexts.input, contexts.game, contexts.data));
        }
    }
}