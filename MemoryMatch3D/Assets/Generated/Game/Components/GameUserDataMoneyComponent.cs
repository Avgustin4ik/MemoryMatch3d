//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Core.UsedData.UserDataComponents.UserDataMoneyComponent userDataMoney { get { return (Core.UsedData.UserDataComponents.UserDataMoneyComponent)GetComponent(GameComponentsLookup.UserDataMoney); } }
    public bool hasUserDataMoney { get { return HasComponent(GameComponentsLookup.UserDataMoney); } }

    public void AddUserDataMoney(int newValue) {
        var index = GameComponentsLookup.UserDataMoney;
        var component = (Core.UsedData.UserDataComponents.UserDataMoneyComponent)CreateComponent(index, typeof(Core.UsedData.UserDataComponents.UserDataMoneyComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceUserDataMoney(int newValue) {
        var index = GameComponentsLookup.UserDataMoney;
        var component = (Core.UsedData.UserDataComponents.UserDataMoneyComponent)CreateComponent(index, typeof(Core.UsedData.UserDataComponents.UserDataMoneyComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveUserDataMoney() {
        RemoveComponent(GameComponentsLookup.UserDataMoney);
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

    static Entitas.IMatcher<GameEntity> _matcherUserDataMoney;

    public static Entitas.IMatcher<GameEntity> UserDataMoney {
        get {
            if (_matcherUserDataMoney == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.UserDataMoney);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherUserDataMoney = matcher;
            }

            return _matcherUserDataMoney;
        }
    }
}
