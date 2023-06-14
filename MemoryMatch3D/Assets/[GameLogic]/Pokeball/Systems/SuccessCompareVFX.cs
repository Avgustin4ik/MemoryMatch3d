using System.Collections.Generic;
using Core.Configs;
using Entitas;
using Unity.Mathematics;
using UnityEngine;

namespace Pokeball
{
    public class SuccessCompareVFX : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _pokabalssSuccessGroup;

        public SuccessCompareVFX(GameContext gameContext, UiContext uiContext) : base(gameContext)
        {
            _pokabalssSuccessGroup = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.IsPokeballOpen));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.MatchEvent.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var vfxPrefab = ConfigsCatalogsManager.GetConfig<GameConfig>().VFXSuccessCompare;
            foreach (var pokeball in _pokabalssSuccessGroup.GetEntities())
            {
                if (pokeball.isMatchDetected == false) continue;
                var vfxInstance = UnityEngine.Object.Instantiate(vfxPrefab, pokeball.transform.value.position,
                    quaternion.identity);
                //PS нужно подвинуть вверх, ближе к камере
                float dy = 1f;
                vfxInstance.transform.position += Vector3.up * dy;
                vfxInstance.Play();
            }
        }
    }
}