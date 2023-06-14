//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CinemachineCamera.Components.PriorityComponent priority { get { return (CinemachineCamera.Components.PriorityComponent)GetComponent(GameComponentsLookup.Priority); } }
    public bool hasPriority { get { return HasComponent(GameComponentsLookup.Priority); } }

    public void AddPriority(int newValue) {
        var index = GameComponentsLookup.Priority;
        var component = (CinemachineCamera.Components.PriorityComponent)CreateComponent(index, typeof(CinemachineCamera.Components.PriorityComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePriority(int newValue) {
        var index = GameComponentsLookup.Priority;
        var component = (CinemachineCamera.Components.PriorityComponent)CreateComponent(index, typeof(CinemachineCamera.Components.PriorityComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePriority() {
        RemoveComponent(GameComponentsLookup.Priority);
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

    static Entitas.IMatcher<GameEntity> _matcherPriority;

    public static Entitas.IMatcher<GameEntity> Priority {
        get {
            if (_matcherPriority == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Priority);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPriority = matcher;
            }

            return _matcherPriority;
        }
    }
}
