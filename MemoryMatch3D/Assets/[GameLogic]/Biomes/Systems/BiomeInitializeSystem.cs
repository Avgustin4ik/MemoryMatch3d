using Entitas;
using UnityEngine;

namespace Biomes
{
    public class BiomeInitializeSystem : IInitializeSystem
    {
        public BiomeInitializeSystem(GameContext contextsGame, DataContext contextsData)
        {
        }

        public void Initialize()
        {
            var biomes = Object.FindObjectsOfType<BiomeView>();
            if(biomes == null) return;
            foreach (var biomeView in biomes)
            {
                biomeView.Init(Contexts.sharedInstance.game.CreateEntity());
            }
        }
    }
}