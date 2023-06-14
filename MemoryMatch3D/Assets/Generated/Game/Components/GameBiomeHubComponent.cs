//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Biomes.BiomeHubComponent biomeHubComponent = new Biomes.BiomeHubComponent();

    public bool isBiomeHub {
        get { return HasComponent(GameComponentsLookup.BiomeHub); }
        set {
            if (value != isBiomeHub) {
                var index = GameComponentsLookup.BiomeHub;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : biomeHubComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherBiomeHub;

    public static Entitas.IMatcher<GameEntity> BiomeHub {
        get {
            if (_matcherBiomeHub == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BiomeHub);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBiomeHub = matcher;
            }

            return _matcherBiomeHub;
        }
    }
}
