//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    static readonly Core.Input.Components.ButtonLoadGameLevelComponent buttonLoadGameLevelComponent = new Core.Input.Components.ButtonLoadGameLevelComponent();

    public bool isButtonLoadGameLevel {
        get { return HasComponent(InputComponentsLookup.ButtonLoadGameLevel); }
        set {
            if (value != isButtonLoadGameLevel) {
                var index = InputComponentsLookup.ButtonLoadGameLevel;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : buttonLoadGameLevelComponent;

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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherButtonLoadGameLevel;

    public static Entitas.IMatcher<InputEntity> ButtonLoadGameLevel {
        get {
            if (_matcherButtonLoadGameLevel == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.ButtonLoadGameLevel);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherButtonLoadGameLevel = matcher;
            }

            return _matcherButtonLoadGameLevel;
        }
    }
}
