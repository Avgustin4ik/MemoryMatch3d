using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Biomes
{
    public class SelectionBiomeZoneSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _biomeZoneGroup;
        private readonly IGroup<GameEntity> _vcamGroup;

        public SelectionBiomeZoneSystem(GameContext contextsGame) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _biomeZoneGroup = contextsGame.GetGroup(GameMatcher.BiomeZone);
            _vcamGroup = contextsGame.GetGroup(GameMatcher.AllOf(GameMatcher.AnimalType, GameMatcher.VirtualCamera));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Selected.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasBiomeZone && entity.isSelected;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var selectedBiomeZone in entities)
            {
                foreach (var biomeZone in _biomeZoneGroup)
                {
                    if(selectedBiomeZone.Equals(biomeZone)) continue;
                    biomeZone.isSelected = false;
                }
                var vcameras = _vcamGroup.GetEntities().Where(vc => vc.animalType.value == selectedBiomeZone.biomeZone.value);
                if (vcameras.Count() > 1)
                    throw new IndexOutOfRangeException(
                        message: $"Too much virtual cameras for one animal ({selectedBiomeZone.biomeZone.value})");
                vcameras.FirstOrDefault().isLive = true;
            }
            
            
            
        }
    }
}