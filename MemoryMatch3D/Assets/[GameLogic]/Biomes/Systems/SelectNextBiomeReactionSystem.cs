using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Biomes
{
    public class SelectNextBiomeReactionSystem : ReactiveSystem<InputEntity>
    {
        private readonly IGroup<GameEntity> _biomeZoneGroup;
        private readonly IGroup<GameEntity> _biomeGroup;

        public SelectNextBiomeReactionSystem(InputContext inputContext, GameContext contextsGame) : base(inputContext)
        {
            _biomeZoneGroup = contextsGame.GetGroup(GameMatcher.AllOf(GameMatcher.BiomeZone,
                GameMatcher.ElementIndex,
                GameMatcher.BiomeHub));
            _biomeGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.BiomeType,
                GameMatcher.ElementIndex));
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.ButtonNextElementRequest.Added(),
                InputMatcher.ButtonPreviousElementRequest.Added());
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var requestEntity in entities)
            {
                var biomeZones = _biomeZoneGroup.GetEntities();
                var biomes = _biomeGroup.GetEntities();
                var selected = biomeZones.FirstOrDefault(x => x.isSelected);
                var targetCollection = selected == null ? biomes : biomeZones;
                selected ??= targetCollection.FirstOrDefault(x => x.isSelected);
                
                if (selected == null) continue;
                {
                    var selectedIndex = selected.elementIndex.value;
                    var nextIndex = requestEntity.isButtonNextElementRequest ? selectedIndex + 1 : selectedIndex - 1;
                    biomeZones.FirstOrDefault(x => x.elementIndex.value.Equals((int)Mathf.Repeat(nextIndex, biomeZones.Length)))!.isSelected = true;
                }
            }
        }
    }
}