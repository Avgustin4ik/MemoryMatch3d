//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Animals.HamsterComponent hamsterComponent = new Animals.HamsterComponent();

    public bool isHamster {
        get { return HasComponent(GameComponentsLookup.Hamster); }
        set {
            if (value != isHamster) {
                var index = GameComponentsLookup.Hamster;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : hamsterComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherHamster;

    public static Entitas.IMatcher<GameEntity> Hamster {
        get {
            if (_matcherHamster == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Hamster);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherHamster = matcher;
            }

            return _matcherHamster;
        }
    }
}
