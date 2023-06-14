//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Grid.GridComponents.GridSizeComponent gridSize { get { return (Grid.GridComponents.GridSizeComponent)GetComponent(GameComponentsLookup.GridSize); } }
    public bool hasGridSize { get { return HasComponent(GameComponentsLookup.GridSize); } }

    public void AddGridSize(UnityEngine.Vector2Int newValue) {
        var index = GameComponentsLookup.GridSize;
        var component = (Grid.GridComponents.GridSizeComponent)CreateComponent(index, typeof(Grid.GridComponents.GridSizeComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGridSize(UnityEngine.Vector2Int newValue) {
        var index = GameComponentsLookup.GridSize;
        var component = (Grid.GridComponents.GridSizeComponent)CreateComponent(index, typeof(Grid.GridComponents.GridSizeComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGridSize() {
        RemoveComponent(GameComponentsLookup.GridSize);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherGridSize;

    public static Entitas.IMatcher<GameEntity> GridSize {
        get {
            if (_matcherGridSize == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GridSize);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGridSize = matcher;
            }

            return _matcherGridSize;
        }
    }
}
