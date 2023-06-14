//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly CinemachineCamera.Components.ActiveBlendComponent activeBlendComponent = new CinemachineCamera.Components.ActiveBlendComponent();

    public bool isActiveBlend {
        get { return HasComponent(GameComponentsLookup.ActiveBlend); }
        set {
            if (value != isActiveBlend) {
                var index = GameComponentsLookup.ActiveBlend;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : activeBlendComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherActiveBlend;

    public static Entitas.IMatcher<GameEntity> ActiveBlend {
        get {
            if (_matcherActiveBlend == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ActiveBlend);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherActiveBlend = matcher;
            }

            return _matcherActiveBlend;
        }
    }
}
