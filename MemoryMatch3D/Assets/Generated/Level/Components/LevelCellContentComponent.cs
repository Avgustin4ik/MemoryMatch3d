//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LevelEntity {

    public Core.GameLevels.CellContentComponent cellContent { get { return (Core.GameLevels.CellContentComponent)GetComponent(LevelComponentsLookup.CellContent); } }
    public bool hasCellContent { get { return HasComponent(LevelComponentsLookup.CellContent); } }

    public void AddCellContent(int[,] newValue) {
        var index = LevelComponentsLookup.CellContent;
        var component = (Core.GameLevels.CellContentComponent)CreateComponent(index, typeof(Core.GameLevels.CellContentComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCellContent(int[,] newValue) {
        var index = LevelComponentsLookup.CellContent;
        var component = (Core.GameLevels.CellContentComponent)CreateComponent(index, typeof(Core.GameLevels.CellContentComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCellContent() {
        RemoveComponent(LevelComponentsLookup.CellContent);
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
public sealed partial class LevelMatcher {

    static Entitas.IMatcher<LevelEntity> _matcherCellContent;

    public static Entitas.IMatcher<LevelEntity> CellContent {
        get {
            if (_matcherCellContent == null) {
                var matcher = (Entitas.Matcher<LevelEntity>)Entitas.Matcher<LevelEntity>.AllOf(LevelComponentsLookup.CellContent);
                matcher.componentNames = LevelComponentsLookup.componentNames;
                _matcherCellContent = matcher;
            }

            return _matcherCellContent;
        }
    }
}
