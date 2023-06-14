//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class UiUnlockProgressEventSystem : Entitas.ReactiveSystem<UiEntity> {

    readonly System.Collections.Generic.List<IUiUnlockProgressListener> _listenerBuffer;

    public UiUnlockProgressEventSystem(Contexts contexts) : base(contexts.ui) {
        _listenerBuffer = new System.Collections.Generic.List<IUiUnlockProgressListener>();
    }

    protected override Entitas.ICollector<UiEntity> GetTrigger(Entitas.IContext<UiEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(UiMatcher.UnlockProgress)
        );
    }

    protected override bool Filter(UiEntity entity) {
        return entity.hasUnlockProgress && entity.hasUiUnlockProgressListener;
    }

    protected override void Execute(System.Collections.Generic.List<UiEntity> entities) {
        foreach (var e in entities) {
            var component = e.unlockProgress;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.uiUnlockProgressListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnUnlockProgress(e, component.value);
            }
        }
    }
}
