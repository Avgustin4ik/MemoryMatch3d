using Core.Extension;
using UnityEngine;

namespace Pokeball
{
    class PokeballModelView : MonoBehAdvGame
    {
        [SerializeField] private Animator animator;
        public event System.Action onAnimationEnd;
        public event System.Action onAnimationBegin;
        public void TriggerAnimationEnd () => onAnimationEnd?.Invoke();
        public void TriggerAnimationBegin () => onAnimationBegin?.Invoke();
        public Animator Animator => animator;
        
        private Renderer[] _meshRenderer;


        private void Awake() 
        {
            _meshRenderer = GetComponentsInChildren<Renderer>(includeInactive:true);
        }

        public void SetActive(bool value = true)
        {
            foreach (var meshRenderer in _meshRenderer)
            {
                meshRenderer.enabled = value;
            }
        }

        

    }
}