using Core.Configs;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Configs/Game/PlayerData")]
    public class PlayerData : Config
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public uint TurnsLimit { get; private set; }
        
    }
}