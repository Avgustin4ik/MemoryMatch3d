
using Core.GameLevels;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using EventType = Entitas.CodeGeneration.Attributes.EventType;

namespace Grid
{
    public class GridComponents
    {
        [Game, Unique]
        public sealed class GridComponent : IComponent
        {
        }

        [Game]
        public sealed class GridSizeComponent : IComponent
        {
            public Vector2Int value;
        }
        [Game]
        public sealed class ClearanceComponent : IComponent
        {
            public Vector2 value;
        }

        [Game, Cleanup(CleanupMode.DestroyEntity),FlagPrefix("Event")]
        public sealed class GridCompleteComponent : IComponent
        {
        }
        [Game, Cleanup(CleanupMode.DestroyEntity),FlagPrefix("Event")]
        public sealed class GridProductionPlacementDoneComponent : IComponent
        {
        }
        
        #region cells

        [Game]
        public sealed class CellComponent : IComponent
        {
        }

        [Game]
        public sealed class RawComponent : IComponent
        {
            public ushort value;
        }

        [Game]
        public sealed class ColumnComponent : IComponent
        {
            public ushort value;
        }

        [Game]
        public sealed class SelectedComponent : IComponent
        {
        }

        [Game]
        public sealed class IndexComponent : IComponent
        {
            [EntityIndex]
            public Vector2Int value;
        }

        [Game, Event(EventTarget.Self)]
        public sealed class InFocusComponent : IComponent
        {
            public bool value;
        }

        [Game]
        public sealed class LinkedPokeballComponent : IComponent
        {
            public int value;
        }

        
        #endregion

        
    }
}