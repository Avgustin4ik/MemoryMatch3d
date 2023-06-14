//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Turn.TurnsLimitByLevelComponent turnsLimitByLevel { get { return (Turn.TurnsLimitByLevelComponent)GetComponent(GameComponentsLookup.TurnsLimitByLevel); } }
    public bool hasTurnsLimitByLevel { get { return HasComponent(GameComponentsLookup.TurnsLimitByLevel); } }

    public void AddTurnsLimitByLevel(uint newValue) {
        var index = GameComponentsLookup.TurnsLimitByLevel;
        var component = (Turn.TurnsLimitByLevelComponent)CreateComponent(index, typeof(Turn.TurnsLimitByLevelComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTurnsLimitByLevel(uint newValue) {
        var index = GameComponentsLookup.TurnsLimitByLevel;
        var component = (Turn.TurnsLimitByLevelComponent)CreateComponent(index, typeof(Turn.TurnsLimitByLevelComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTurnsLimitByLevel() {
        RemoveComponent(GameComponentsLookup.TurnsLimitByLevel);
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

    static Entitas.IMatcher<GameEntity> _matcherTurnsLimitByLevel;

    public static Entitas.IMatcher<GameEntity> TurnsLimitByLevel {
        get {
            if (_matcherTurnsLimitByLevel == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TurnsLimitByLevel);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTurnsLimitByLevel = matcher;
            }

            return _matcherTurnsLimitByLevel;
        }
    }
}
