using System.Linq;
using Animals;
using Pokeball;
using UnityEngine;

namespace Biomes
{
    [CreateAssetMenu(fileName = "New Biome Catalog", menuName = "Game/Catalogs/Biome Catalog", order = 0)]
    public class BiomeCatalog : ScriptableObject
    {
        [field: SerializeField] public Biomes BiomeType { get; private set; }
        [SerializeField] private AnimalView[] biomeAnimalViews;

        public AnimalView GetRandomAnimalView()
        {
            return biomeAnimalViews[Random.Range(0, biomeAnimalViews.Length)];
        }

        public AnimalView GetAnimal(AnimalsType type) => biomeAnimalViews.FirstOrDefault(x => x.animalType == type);
    }
}