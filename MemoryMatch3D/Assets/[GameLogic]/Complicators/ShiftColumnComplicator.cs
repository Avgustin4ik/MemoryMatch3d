using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Complicators
{
    [Serializable]
    [CreateAssetMenu(menuName = "Complicator/ShiftColumnComplicator", fileName = "ShiftColumnComplicator", order = 0)]
    public class ShiftColumnComplicator : ComplicatorData
    {
        public override ComplicatorType Type() => ComplicatorType.SHIFT_COLUMN;

        /// <summary>
        /// DefaultDirection = Up => Down
        /// </summary>
        public bool IsDirectionDefault = true;
        [SerializeField] private int _targetColumnIndex;
        public int TargetColumnIndex
        {
            get => _targetColumnIndex;
            set
            {
                if (value < 0) RandomizeTarget = true;
                _targetColumnIndex = value;
            }
        }
        
    }
}