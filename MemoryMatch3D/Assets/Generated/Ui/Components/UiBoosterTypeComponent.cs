//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UiEntity {

    public Ui.BoosterTypeComponent boosterType { get { return (Ui.BoosterTypeComponent)GetComponent(UiComponentsLookup.BoosterType); } }
    public bool hasBoosterType { get { return HasComponent(UiComponentsLookup.BoosterType); } }

    public void AddBoosterType(System.Type newValue) {
        var index = UiComponentsLookup.BoosterType;
        var component = (Ui.BoosterTypeComponent)CreateComponent(index, typeof(Ui.BoosterTypeComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBoosterType(System.Type newValue) {
        var index = UiComponentsLookup.BoosterType;
        var component = (Ui.BoosterTypeComponent)CreateComponent(index, typeof(Ui.BoosterTypeComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBoosterType() {
        RemoveComponent(UiComponentsLookup.BoosterType);
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

    static Entitas.IMatcher<UiEntity> _matcherBoosterType;

    public static Entitas.IMatcher<UiEntity> BoosterType {
        get {
            if (_matcherBoosterType == null) {
                var matcher = (Entitas.Matcher<UiEntity>)Entitas.Matcher<UiEntity>.AllOf(UiComponentsLookup.BoosterType);
                matcher.componentNames = UiComponentsLookup.componentNames;
                _matcherBoosterType = matcher;
            }

            return _matcherBoosterType;
        }
    }
}
