using Core.Extension;
using Entitas;
using UnityEngine;

namespace Biomes
{
    class BiomeView : MonoBehAdvGame
    {
        [SerializeField] private Biomes type;
        [SerializeField] private BiomeZoneView[] biomeZones;

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.AddBiomeType(type);
            GameEntity.isHouseBiome = true; //todo: clean up
            int index = 0;
            foreach (var biomeZoneView in biomeZones)
            {
                var biomeZoneEntity = Contexts.sharedInstance.game.CreateEntity();
                biomeZoneView.Init(biomeZoneEntity);
                biomeZoneEntity.AddBiomeType(type);
                biomeZoneEntity.AddElementIndex(index++);
            }
        }
    }
}