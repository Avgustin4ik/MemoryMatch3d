//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly _GameCore_.Common.StateMachine.Components.StateTransitionComponent stateTransitionComponent = new _GameCore_.Common.StateMachine.Components.StateTransitionComponent();

    public bool isStateTransition {
        get { return HasComponent(GameComponentsLookup.StateTransition); }
        set {
            if (value != isStateTransition) {
                var index = GameComponentsLookup.StateTransition;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : stateTransitionComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherStateTransition;

    public static Entitas.IMatcher<GameEntity> StateTransition {
        get {
            if (_matcherStateTransition == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.StateTransition);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherStateTransition = matcher;
            }

            return _matcherStateTransition;
        }
    }
}
