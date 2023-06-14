using System;
using UnityEngine;

namespace Complicators
{
    [Serializable]
    [CreateAssetMenu(menuName = "Complicator/SwitchPokeballsComplicator", fileName = "SwitchPokeballsComplicator", order = 3)]
    public class SwitchPokeballsComplicator : ComplicatorData
    {
        public override ComplicatorType Type() => ComplicatorType.SWITCH_TWO_POKEBALLS;
        [SerializeField] private Vector2Int _targetACellIndex;
        [SerializeField] private Vector2Int _targetBCellIndex;
        
        public Vector2Int TargetACellIndex
        {
            get => _targetACellIndex;
            set
            {
                RandomizeTarget = value == _targetBCellIndex;
                _targetACellIndex = value;   
            }
        }

        public Vector2Int TargetBCellIndex
        {
            get => _targetBCellIndex;
            set
            {
                RandomizeTarget = value == _targetACellIndex;
                _targetBCellIndex = value;
            }
        }
    }
}