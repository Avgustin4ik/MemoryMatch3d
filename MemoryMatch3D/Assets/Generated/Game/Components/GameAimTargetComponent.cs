//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Boosters.AimTargetComponent aimTarget { get { return (Boosters.AimTargetComponent)GetComponent(GameComponentsLookup.AimTarget); } }
    public bool hasAimTarget { get { return HasComponent(GameComponentsLookup.AimTarget); } }

    public void AddAimTarget(Boosters.AimTarget newValue) {
        var index = GameComponentsLookup.AimTarget;
        var component = (Boosters.AimTargetComponent)CreateComponent(index, typeof(Boosters.AimTargetComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAimTarget(Boosters.AimTarget newValue) {
        var index = GameComponentsLookup.AimTarget;
        var component = (Boosters.AimTargetComponent)CreateComponent(index, typeof(Boosters.AimTargetComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAimTarget() {
        RemoveComponent(GameComponentsLookup.AimTarget);
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

    static Entitas.IMatcher<GameEntity> _matcherAimTarget;

    public static Entitas.IMatcher<GameEntity> AimTarget {
        get {
            if (_matcherAimTarget == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AimTarget);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAimTarget = matcher;
            }

            return _matcherAimTarget;
        }
    }
}
