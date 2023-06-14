//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UiEntity {

    public AnyUiTimerListenerComponent anyUiTimerListener { get { return (AnyUiTimerListenerComponent)GetComponent(UiComponentsLookup.AnyUiTimerListener); } }
    public bool hasAnyUiTimerListener { get { return HasComponent(UiComponentsLookup.AnyUiTimerListener); } }

    public void AddAnyUiTimerListener(System.Collections.Generic.List<IAnyUiTimerListener> newValue) {
        var index = UiComponentsLookup.AnyUiTimerListener;
        var component = (AnyUiTimerListenerComponent)CreateComponent(index, typeof(AnyUiTimerListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnyUiTimerListener(System.Collections.Generic.List<IAnyUiTimerListener> newValue) {
        var index = UiComponentsLookup.AnyUiTimerListener;
        var component = (AnyUiTimerListenerComponent)CreateComponent(index, typeof(AnyUiTimerListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnyUiTimerListener() {
        RemoveComponent(UiComponentsLookup.AnyUiTimerListener);
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
public sealed partial class UiMatcher {

    static Entitas.IMatcher<UiEntity> _matcherAnyUiTimerListener;

    public static Entitas.IMatcher<UiEntity> AnyUiTimerListener {
        get {
            if (_matcherAnyUiTimerListener == null) {
                var matcher = (Entitas.Matcher<UiEntity>)Entitas.Matcher<UiEntity>.AllOf(UiComponentsLookup.AnyUiTimerListener);
                matcher.componentNames = UiComponentsLookup.componentNames;
                _matcherAnyUiTimerListener = matcher;
            }

            return _matcherAnyUiTimerListener;
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
public partial class UiEntity {

    public void AddAnyUiTimerListener(IAnyUiTimerListener value) {
        var listeners = hasAnyUiTimerListener
            ? anyUiTimerListener.value
            : new System.Collections.Generic.List<IAnyUiTimerListener>();
        listeners.Add(value);
        ReplaceAnyUiTimerListener(listeners);
    }

    public void RemoveAnyUiTimerListener(IAnyUiTimerListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anyUiTimerListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnyUiTimerListener();
        } else {
            ReplaceAnyUiTimerListener(listeners);
        }
    }
}
