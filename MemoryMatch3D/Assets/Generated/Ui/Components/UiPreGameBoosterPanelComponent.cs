//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UiEntity {

    static readonly Ui.PreGameBoosterPanelComponent preGameBoosterPanelComponent = new Ui.PreGameBoosterPanelComponent();

    public bool isPreGameBoosterPanel {
        get { return HasComponent(UiComponentsLookup.PreGameBoosterPanel); }
        set {
            if (value != isPreGameBoosterPanel) {
                var index = UiComponentsLookup.PreGameBoosterPanel;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : preGameBoosterPanelComponent;

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
public sealed partial class UiMatcher {

    static Entitas.IMatcher<UiEntity> _matcherPreGameBoosterPanel;

    public static Entitas.IMatcher<UiEntity> PreGameBoosterPanel {
        get {
            if (_matcherPreGameBoosterPanel == null) {
                var matcher = (Entitas.Matcher<UiEntity>)Entitas.Matcher<UiEntity>.AllOf(UiComponentsLookup.PreGameBoosterPanel);
                matcher.componentNames = UiComponentsLookup.componentNames;
                _matcherPreGameBoosterPanel = matcher;
            }

            return _matcherPreGameBoosterPanel;
        }
    }
}
