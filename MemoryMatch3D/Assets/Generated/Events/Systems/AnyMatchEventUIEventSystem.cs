//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class AnyMatchEventUIEventSystem : Entitas.ReactiveSystem<UiEntity> {

    readonly Entitas.IGroup<UiEntity> _listeners;
    readonly System.Collections.Generic.List<UiEntity> _entityBuffer;
    readonly System.Collections.Generic.List<IAnyMatchEventUIListener> _listenerBuffer;

    public AnyMatchEventUIEventSystem(Contexts contexts) : base(contexts.ui) {
        _listeners = contexts.ui.GetGroup(UiMatcher.AnyMatchEventUIListener);
        _entityBuffer = new System.Collections.Generic.List<UiEntity>();
        _listenerBuffer = new System.Collections.Generic.List<IAnyMatchEventUIListener>();
    }

    protected override Entitas.ICollector<UiEntity> GetTrigger(Entitas.IContext<UiEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(UiMatcher.MatchEventUI)
        );
    }

    protected override bool Filter(UiEntity entity) {
        return entity.isMatchEventUI;
    }

    protected override void Execute(System.Collections.Generic.List<UiEntity> entities) {
        foreach (var e in entities) {
            
            foreach (var listenerEntity in _listeners.GetEntities(_entityBuffer)) {
                _listenerBuffer.Clear();
                _listenerBuffer.AddRange(listenerEntity.anyMatchEventUIListener.value);
                foreach (var listener in _listenerBuffer) {
                    listener.OnAnyMatchEventUI(e);
                }
            }
        }
    }
}
