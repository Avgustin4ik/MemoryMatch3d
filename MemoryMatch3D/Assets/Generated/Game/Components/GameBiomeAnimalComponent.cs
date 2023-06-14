//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Animals.BiomeAnimals.Components.BiomeAnimalComponent biomeAnimalComponent = new Animals.BiomeAnimals.Components.BiomeAnimalComponent();

    public bool isBiomeAnimal {
        get { return HasComponent(GameComponentsLookup.BiomeAnimal); }
        set {
            if (value != isBiomeAnimal) {
                var index = GameComponentsLookup.BiomeAnimal;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : biomeAnimalComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherBiomeAnimal;

    public static Entitas.IMatcher<GameEntity> BiomeAnimal {
        get {
            if (_matcherBiomeAnimal == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BiomeAnimal);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBiomeAnimal = matcher;
            }

            return _matcherBiomeAnimal;
        }
    }
}
