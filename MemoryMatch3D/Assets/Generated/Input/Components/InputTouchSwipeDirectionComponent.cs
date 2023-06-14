//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public Core.Input.TouchSwipeDirectionComponent touchSwipeDirection { get { return (Core.Input.TouchSwipeDirectionComponent)GetComponent(InputComponentsLookup.TouchSwipeDirection); } }
    public bool hasTouchSwipeDirection { get { return HasComponent(InputComponentsLookup.TouchSwipeDirection); } }

    public void AddTouchSwipeDirection(Core.Input.SwipeDirection newValue) {
        var index = InputComponentsLookup.TouchSwipeDirection;
        var component = (Core.Input.TouchSwipeDirectionComponent)CreateComponent(index, typeof(Core.Input.TouchSwipeDirectionComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTouchSwipeDirection(Core.Input.SwipeDirection newValue) {
        var index = InputComponentsLookup.TouchSwipeDirection;
        var component = (Core.Input.TouchSwipeDirectionComponent)CreateComponent(index, typeof(Core.Input.TouchSwipeDirectionComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTouchSwipeDirection() {
        RemoveComponent(InputComponentsLookup.TouchSwipeDirection);
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

    static Entitas.IMatcher<InputEntity> _matcherTouchSwipeDirection;

    public static Entitas.IMatcher<InputEntity> TouchSwipeDirection {
        get {
            if (_matcherTouchSwipeDirection == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.TouchSwipeDirection);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherTouchSwipeDirection = matcher;
            }

            return _matcherTouchSwipeDirection;
        }
    }
}
