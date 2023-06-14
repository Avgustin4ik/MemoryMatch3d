//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UiEntity {

    static readonly Core.UI.DefeatScreenComponent defeatScreenComponent = new Core.UI.DefeatScreenComponent();

    public bool isDefeatScreen {
        get { return HasComponent(UiComponentsLookup.DefeatScreen); }
        set {
            if (value != isDefeatScreen) {
                var index = UiComponentsLookup.DefeatScreen;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : defeatScreenComponent;

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

    static Entitas.IMatcher<UiEntity> _matcherDefeatScreen;

    public static Entitas.IMatcher<UiEntity> DefeatScreen {
        get {
            if (_matcherDefeatScreen == null) {
                var matcher = (Entitas.Matcher<UiEntity>)Entitas.Matcher<UiEntity>.AllOf(UiComponentsLookup.DefeatScreen);
                matcher.componentNames = UiComponentsLookup.componentNames;
                _matcherDefeatScreen = matcher;
            }

            return _matcherDefeatScreen;
        }
    }
}
