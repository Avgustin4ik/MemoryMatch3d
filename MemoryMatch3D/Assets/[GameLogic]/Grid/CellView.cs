using System;
using Core.Extension;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Entitas;
using UnityEngine;

namespace Grid
{
    public class CellView : MonoBehAdvGameLevelCleanup, IGameClickedListener, IInFocusListener
    {
        private MeshRenderer _meshRenderer;
        private Outline _outline;
        private Material _defaultMaterial;
        private TweenerCore<Color,Color,ColorOptions> _blinkTween;

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.isCell = true;
            GameEntity.AddGameClickedListener(this);
            GameEntity.AddInFocus(false);
            GameEntity.AddInFocusListener(this);
            _meshRenderer = GetComponent<MeshRenderer>();
            TryGetComponent<Outline>(out _outline);
            _defaultMaterial = _meshRenderer.sharedMaterial;
            _blinkTween = _meshRenderer.material.DOColor(Color.yellow,0.5f)
                .SetLoops(2,LoopType.Yoyo)
                .Pause()
                .SetAutoKill(false)
                .SetLink(gameObject);
        }

        public void OnClicked(GameEntity entity)
        {
            if(_blinkTween.IsPlaying()) return;
            _blinkTween.Restart();
        }

        public void OnInFocus(GameEntity entity, bool value)
        {
            if(_outline == null) return;
            _outline.enabled = value;
        }
    }
}
