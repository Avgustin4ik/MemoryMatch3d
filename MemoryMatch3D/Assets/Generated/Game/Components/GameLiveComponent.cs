//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly CinemachineCamera.Components.LiveComponent liveComponent = new CinemachineCamera.Components.LiveComponent();

    public bool isLive {
        get { return HasComponent(GameComponentsLookup.Live); }
        set {
            if (value != isLive) {
                var index = GameComponentsLookup.Live;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : liveComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherLive;

    public static Entitas.IMatcher<GameEntity> Live {
        get {
            if (_matcherLive == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Live);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLive = matcher;
            }

            return _matcherLive;
        }
    }
}
