//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UiEntity {

    public AnyUserDataMoneyDisplayListenerComponent anyUserDataMoneyDisplayListener { get { return (AnyUserDataMoneyDisplayListenerComponent)GetComponent(UiComponentsLookup.AnyUserDataMoneyDisplayListener); } }
    public bool hasAnyUserDataMoneyDisplayListener { get { return HasComponent(UiComponentsLookup.AnyUserDataMoneyDisplayListener); } }

    public void AddAnyUserDataMoneyDisplayListener(System.Collections.Generic.List<IAnyUserDataMoneyDisplayListener> newValue) {
        var index = UiComponentsLookup.AnyUserDataMoneyDisplayListener;
        var component = (AnyUserDataMoneyDisplayListenerComponent)CreateComponent(index, typeof(AnyUserDataMoneyDisplayListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnyUserDataMoneyDisplayListener(System.Collections.Generic.List<IAnyUserDataMoneyDisplayListener> newValue) {
        var index = UiComponentsLookup.AnyUserDataMoneyDisplayListener;
        var component = (AnyUserDataMoneyDisplayListenerComponent)CreateComponent(index, typeof(AnyUserDataMoneyDisplayListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnyUserDataMoneyDisplayListener() {
        RemoveComponent(UiComponentsLookup.AnyUserDataMoneyDisplayListener);
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

    static Entitas.IMatcher<UiEntity> _matcherAnyUserDataMoneyDisplayListener;

    public static Entitas.IMatcher<UiEntity> AnyUserDataMoneyDisplayListener {
        get {
            if (_matcherAnyUserDataMoneyDisplayListener == null) {
                var matcher = (Entitas.Matcher<UiEntity>)Entitas.Matcher<UiEntity>.AllOf(UiComponentsLookup.AnyUserDataMoneyDisplayListener);
                matcher.componentNames = UiComponentsLookup.componentNames;
                _matcherAnyUserDataMoneyDisplayListener = matcher;
            }

            return _matcherAnyUserDataMoneyDisplayListener;
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

    public void AddAnyUserDataMoneyDisplayListener(IAnyUserDataMoneyDisplayListener value) {
        var listeners = hasAnyUserDataMoneyDisplayListener
            ? anyUserDataMoneyDisplayListener.value
            : new System.Collections.Generic.List<IAnyUserDataMoneyDisplayListener>();
        listeners.Add(value);
        ReplaceAnyUserDataMoneyDisplayListener(listeners);
    }

    public void RemoveAnyUserDataMoneyDisplayListener(IAnyUserDataMoneyDisplayListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anyUserDataMoneyDisplayListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnyUserDataMoneyDisplayListener();
        } else {
            ReplaceAnyUserDataMoneyDisplayListener(listeners);
        }
    }
}
