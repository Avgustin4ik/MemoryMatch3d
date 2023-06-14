using System;
using UnityEngine;

namespace Complicators
{
    [Serializable]
    public abstract class ComplicatorData : ScriptableObject
    {
        public bool RandomizeTarget = true;
        public abstract ComplicatorType Type();
        public bool IsRepeatable = false;
        
        [SerializeField] private uint _repeatFrequency = 0;
        public uint RepeatFrequency
        {
            get => _repeatFrequency;
            set
            {
                if (value == 0) IsRepeatable = false;
                _repeatFrequency = value;
            }
        }
    }
    public enum ComplicatorType
    {
        DEFAULT= 0,
        SHIFT_COLUMN = 1,
        SHIFT_ROW  = 2,
        SWITCH_TWO_POKEBALLS = 3
    }
}

