//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GameCamera.GameCameraDetectionOffsetComponent gameCameraDetectionOffset { get { return (GameCamera.GameCameraDetectionOffsetComponent)GetComponent(GameComponentsLookup.GameCameraDetectionOffset); } }
    public bool hasGameCameraDetectionOffset { get { return HasComponent(GameComponentsLookup.GameCameraDetectionOffset); } }

    public void AddGameCameraDetectionOffset(float newValue) {
        var index = GameComponentsLookup.GameCameraDetectionOffset;
        var component = (GameCamera.GameCameraDetectionOffsetComponent)CreateComponent(index, typeof(GameCamera.GameCameraDetectionOffsetComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGameCameraDetectionOffset(float newValue) {
        var index = GameComponentsLookup.GameCameraDetectionOffset;
        var component = (GameCamera.GameCameraDetectionOffsetComponent)CreateComponent(index, typeof(GameCamera.GameCameraDetectionOffsetComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGameCameraDetectionOffset() {
        RemoveComponent(GameComponentsLookup.GameCameraDetectionOffset);
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

    static Entitas.IMatcher<GameEntity> _matcherGameCameraDetectionOffset;

    public static Entitas.IMatcher<GameEntity> GameCameraDetectionOffset {
        get {
            if (_matcherGameCameraDetectionOffset == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GameCameraDetectionOffset);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGameCameraDetectionOffset = matcher;
            }

            return _matcherGameCameraDetectionOffset;
        }
    }
}
