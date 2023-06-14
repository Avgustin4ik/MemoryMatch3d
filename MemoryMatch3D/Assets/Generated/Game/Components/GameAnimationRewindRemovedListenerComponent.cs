//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AnimationRewindRemovedListenerComponent animationRewindRemovedListener { get { return (AnimationRewindRemovedListenerComponent)GetComponent(GameComponentsLookup.AnimationRewindRemovedListener); } }
    public bool hasAnimationRewindRemovedListener { get { return HasComponent(GameComponentsLookup.AnimationRewindRemovedListener); } }

    public void AddAnimationRewindRemovedListener(System.Collections.Generic.List<IAnimationRewindRemovedListener> newValue) {
        var index = GameComponentsLookup.AnimationRewindRemovedListener;
        var component = (AnimationRewindRemovedListenerComponent)CreateComponent(index, typeof(AnimationRewindRemovedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnimationRewindRemovedListener(System.Collections.Generic.List<IAnimationRewindRemovedListener> newValue) {
        var index = GameComponentsLookup.AnimationRewindRemovedListener;
        var component = (AnimationRewindRemovedListenerComponent)CreateComponent(index, typeof(AnimationRewindRemovedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnimationRewindRemovedListener() {
        RemoveComponent(GameComponentsLookup.AnimationRewindRemovedListener);
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

    static Entitas.IMatcher<GameEntity> _matcherAnimationRewindRemovedListener;

    public static Entitas.IMatcher<GameEntity> AnimationRewindRemovedListener {
        get {
            if (_matcherAnimationRewindRemovedListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AnimationRewindRemovedListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAnimationRewindRemovedListener = matcher;
            }

            return _matcherAnimationRewindRemovedListener;
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

    public void AddAnimationRewindRemovedListener(IAnimationRewindRemovedListener value) {
        var listeners = hasAnimationRewindRemovedListener
            ? animationRewindRemovedListener.value
            : new System.Collections.Generic.List<IAnimationRewindRemovedListener>();
        listeners.Add(value);
        ReplaceAnimationRewindRemovedListener(listeners);
    }

    public void RemoveAnimationRewindRemovedListener(IAnimationRewindRemovedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = animationRewindRemovedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnimationRewindRemovedListener();
        } else {
            ReplaceAnimationRewindRemovedListener(listeners);
        }
    }
}
