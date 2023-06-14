using System.Linq;
using Biomes;
using Core.Configs;
using Pokeball;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game/GameConfig")]
public sealed class GameConfig : Config
{
    public const string PlayerDataKey = "playerData";
    [field: SerializeField] public int OpenItemsLimit { get; private set; }
    [field: SerializeField] public int SoftRewardSuccessCompare { get; private set; }
    [field: SerializeField] public float TimerIntroMainGame { get; private set; }
    [field: SerializeField] public float TimerWrongCompare { get; private set; }
    [field: SerializeField] public float TimerSuccessCompare { get; private set; }
    [field: SerializeField] public ParticleSystem VFXSuccessCompare { get; private set; }

    [field: Header("Grid")]
    private Vector2 _cellSize;

    [SerializeField] private Grid.CellView _cellPrefab;
    public Grid.CellView CellPrefab
    {
        get => _cellPrefab;
    }
    
    [field: Header("Balance")]
    [field: SerializeField] public uint TurnsByLevelLimit { get; private set; }
    [field: SerializeField] public bool RandomizeByLevelRestart { get; private set; }
    
    [SerializeField] private Biomes.BiomeCatalog[] biomeCatalogs;
    [field: SerializeField] public PokeballView PokeballPrefab { get; private set; }
    public BiomeCatalog GetBiomeCatalog(Biomes.Biomes biomeType) => biomeCatalogs.FirstOrDefault(x => x.BiomeType == biomeType);
}