using System;
using UnityEngine;

namespace Complicators
{
    [Serializable]
    [CreateAssetMenu(menuName = "Complicator/ShiftRowComplicator", fileName = "ShiftRowComplicator", order = 1)]
    public class ShiftRowComplicator : ComplicatorData
    {
        public override ComplicatorType Type() => ComplicatorType.SHIFT_ROW;
        /// <summary>
        /// DefaultDirection = Left => Rigth
        /// </summary>
        public bool IsDirectionDefault = true;
        [SerializeField] private int _targetRowIndex;

        public int TargetRowIndex
        {
            get => _targetRowIndex;
            set
            {
                RandomizeTarget = value < 0;
                _targetRowIndex = value;
            }
        }
    }
}