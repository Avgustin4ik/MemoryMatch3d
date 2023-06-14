using Core.Configs;
using Player;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayersConfig", menuName = "Configs/Game/PlayersConfig")]

    public class PlayersConfig : Config
    {
        [field: SerializeField] public PlayerData[] PlayersCollection { get; private set; }
    }
}