using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
    [CreateAssetMenu(fileName = "Booster", menuName = "Configs/Game/Booster")]
    public class BoosterData : ScriptableObject
    {
        [field: SerializeField] public Image BoosterIcon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public short ID { get; private set; }
        [field: SerializeField] public AimTarget AimType { get; private set; }
    }
}