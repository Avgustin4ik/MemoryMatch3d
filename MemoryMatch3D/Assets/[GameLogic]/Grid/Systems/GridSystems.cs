using System;
using Unity.Mathematics;

namespace Grid
{
    public class GridSystems : Feature
    {
        public GridSystems(Contexts contexts)
        {
            // Add(new ReadLevelsBlueprintsInitializationSystem(contexts.level));
            Add(new GridInitializeSystem(contexts.game));
            Add(new PlaceProductOnCellSystem(contexts.game, contexts.level, contexts.data));
            Add(new CellTriggerPokeballSystem(contexts.game));
        }
    }
}