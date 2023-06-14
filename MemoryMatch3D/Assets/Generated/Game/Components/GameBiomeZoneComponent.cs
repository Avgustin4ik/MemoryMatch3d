//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Biomes.BiomeZoneComponent biomeZone { get { return (Biomes.BiomeZoneComponent)GetComponent(GameComponentsLookup.BiomeZone); } }
    public bool hasBiomeZone { get { return HasComponent(GameComponentsLookup.BiomeZone); } }

    public void AddBiomeZone(Animals.AnimalsType newValue) {
        var index = GameComponentsLookup.BiomeZone;
        var component = (Biomes.BiomeZoneComponent)CreateComponent(index, typeof(Biomes.BiomeZoneComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBiomeZone(Animals.AnimalsType newValue) {
        var index = GameComponentsLookup.BiomeZone;
        var component = (Biomes.BiomeZoneComponent)CreateComponent(index, typeof(Biomes.BiomeZoneComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBiomeZone() {
        RemoveComponent(GameComponentsLookup.BiomeZone);
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

    static Entitas.IMatcher<GameEntity> _matcherBiomeZone;

    public static Entitas.IMatcher<GameEntity> BiomeZone {
        get {
            if (_matcherBiomeZone == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BiomeZone);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBiomeZone = matcher;
            }

            return _matcherBiomeZone;
        }
    }
}
