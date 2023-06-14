//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Player.PlayerTurnsLeftComponent playerTurnsLeft { get { return (Player.PlayerTurnsLeftComponent)GetComponent(GameComponentsLookup.PlayerTurnsLeft); } }
    public bool hasPlayerTurnsLeft { get { return HasComponent(GameComponentsLookup.PlayerTurnsLeft); } }

    public void AddPlayerTurnsLeft(uint newValue) {
        var index = GameComponentsLookup.PlayerTurnsLeft;
        var component = (Player.PlayerTurnsLeftComponent)CreateComponent(index, typeof(Player.PlayerTurnsLeftComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePlayerTurnsLeft(uint newValue) {
        var index = GameComponentsLookup.PlayerTurnsLeft;
        var component = (Player.PlayerTurnsLeftComponent)CreateComponent(index, typeof(Player.PlayerTurnsLeftComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePlayerTurnsLeft() {
        RemoveComponent(GameComponentsLookup.PlayerTurnsLeft);
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

    static Entitas.IMatcher<GameEntity> _matcherPlayerTurnsLeft;

    public static Entitas.IMatcher<GameEntity> PlayerTurnsLeft {
        get {
            if (_matcherPlayerTurnsLeft == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PlayerTurnsLeft);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlayerTurnsLeft = matcher;
            }

            return _matcherPlayerTurnsLeft;
        }
    }
}
