//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PriorityListenerComponent priorityListener { get { return (PriorityListenerComponent)GetComponent(GameComponentsLookup.PriorityListener); } }
    public bool hasPriorityListener { get { return HasComponent(GameComponentsLookup.PriorityListener); } }

    public void AddPriorityListener(System.Collections.Generic.List<IPriorityListener> newValue) {
        var index = GameComponentsLookup.PriorityListener;
        var component = (PriorityListenerComponent)CreateComponent(index, typeof(PriorityListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePriorityListener(System.Collections.Generic.List<IPriorityListener> newValue) {
        var index = GameComponentsLookup.PriorityListener;
        var component = (PriorityListenerComponent)CreateComponent(index, typeof(PriorityListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePriorityListener() {
        RemoveComponent(GameComponentsLookup.PriorityListener);
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

    static Entitas.IMatcher<GameEntity> _matcherPriorityListener;

    public static Entitas.IMatcher<GameEntity> PriorityListener {
        get {
            if (_matcherPriorityListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PriorityListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPriorityListener = matcher;
            }

            return _matcherPriorityListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddPriorityListener(IPriorityListener value) {
        var listeners = hasPriorityListener
            ? priorityListener.value
            : new System.Collections.Generic.List<IPriorityListener>();
        listeners.Add(value);
        ReplacePriorityListener(listeners);
    }

    public void RemovePriorityListener(IPriorityListener value, bool removeComponentWhenEmpty = true) {
        var listeners = priorityListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemovePriorityListener();
        } else {
            ReplacePriorityListener(listeners);
        }
    }
}
