//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UiEntity {

    static readonly Ui.PreGameScreenComponent preGameScreenComponent = new Ui.PreGameScreenComponent();

    public bool isPreGameScreen {
        get { return HasComponent(UiComponentsLookup.PreGameScreen); }
        set {
            if (value != isPreGameScreen) {
                var index = UiComponentsLookup.PreGameScreen;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : preGameScreenComponent;

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

    static Entitas.IMatcher<UiEntity> _matcherPreGameScreen;

    public static Entitas.IMatcher<UiEntity> PreGameScreen {
        get {
            if (_matcherPreGameScreen == null) {
                var matcher = (Entitas.Matcher<UiEntity>)Entitas.Matcher<UiEntity>.AllOf(UiComponentsLookup.PreGameScreen);
                matcher.componentNames = UiComponentsLookup.componentNames;
                _matcherPreGameScreen = matcher;
            }

            return _matcherPreGameScreen;
        }
    }
}
