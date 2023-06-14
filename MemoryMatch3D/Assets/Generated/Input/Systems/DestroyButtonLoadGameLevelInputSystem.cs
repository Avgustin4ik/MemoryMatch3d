//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.Roslyn.CodeGeneration.Plugins.CleanupSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.Collections.Generic;
using Entitas;

public sealed class DestroyButtonLoadGameLevelInputSystem : ICleanupSystem {

    readonly IGroup<InputEntity> _group;
    readonly List<InputEntity> _buffer = new List<InputEntity>();

    public DestroyButtonLoadGameLevelInputSystem(Contexts contexts) {
        _group = contexts.input.GetGroup(InputMatcher.ButtonLoadGameLevel);
    }

    public void Cleanup() {
        foreach (var e in _group.GetEntities(_buffer)) {
            e.Destroy();
        }
    }
}
