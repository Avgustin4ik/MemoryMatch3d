//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Grid.GridComponents.SelectedComponent selectedComponent = new Grid.GridComponents.SelectedComponent();

    public bool isSelected {
        get { return HasComponent(GameComponentsLookup.Selected); }
        set {
            if (value != isSelected) {
                var index = GameComponentsLookup.Selected;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : selectedComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherSelected;

    public static Entitas.IMatcher<GameEntity> Selected {
        get {
            if (_matcherSelected == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Selected);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSelected = matcher;
            }

            return _matcherSelected;
        }
    }
}
