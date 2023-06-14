//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class DataEntity {

    public DataUnlockGoalLimitListenerComponent dataUnlockGoalLimitListener { get { return (DataUnlockGoalLimitListenerComponent)GetComponent(DataComponentsLookup.DataUnlockGoalLimitListener); } }
    public bool hasDataUnlockGoalLimitListener { get { return HasComponent(DataComponentsLookup.DataUnlockGoalLimitListener); } }

    public void AddDataUnlockGoalLimitListener(System.Collections.Generic.List<IDataUnlockGoalLimitListener> newValue) {
        var index = DataComponentsLookup.DataUnlockGoalLimitListener;
        var component = (DataUnlockGoalLimitListenerComponent)CreateComponent(index, typeof(DataUnlockGoalLimitListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDataUnlockGoalLimitListener(System.Collections.Generic.List<IDataUnlockGoalLimitListener> newValue) {
        var index = DataComponentsLookup.DataUnlockGoalLimitListener;
        var component = (DataUnlockGoalLimitListenerComponent)CreateComponent(index, typeof(DataUnlockGoalLimitListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDataUnlockGoalLimitListener() {
        RemoveComponent(DataComponentsLookup.DataUnlockGoalLimitListener);
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
public sealed partial class DataMatcher {

    static Entitas.IMatcher<DataEntity> _matcherDataUnlockGoalLimitListener;

    public static Entitas.IMatcher<DataEntity> DataUnlockGoalLimitListener {
        get {
            if (_matcherDataUnlockGoalLimitListener == null) {
                var matcher = (Entitas.Matcher<DataEntity>)Entitas.Matcher<DataEntity>.AllOf(DataComponentsLookup.DataUnlockGoalLimitListener);
                matcher.componentNames = DataComponentsLookup.componentNames;
                _matcherDataUnlockGoalLimitListener = matcher;
            }

            return _matcherDataUnlockGoalLimitListener;
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
public partial class DataEntity {

    public void AddDataUnlockGoalLimitListener(IDataUnlockGoalLimitListener value) {
        var listeners = hasDataUnlockGoalLimitListener
            ? dataUnlockGoalLimitListener.value
            : new System.Collections.Generic.List<IDataUnlockGoalLimitListener>();
        listeners.Add(value);
        ReplaceDataUnlockGoalLimitListener(listeners);
    }

    public void RemoveDataUnlockGoalLimitListener(IDataUnlockGoalLimitListener value, bool removeComponentWhenEmpty = true) {
        var listeners = dataUnlockGoalLimitListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveDataUnlockGoalLimitListener();
        } else {
            ReplaceDataUnlockGoalLimitListener(listeners);
        }
    }
}
