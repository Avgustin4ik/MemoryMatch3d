using Core.Configs;
using UnityEngine;

[CreateAssetMenu(fileName = "DebugLogConfig", menuName = "Configs/DebugLogConfig")]
public sealed class DebugLogConfig : Config
{
    [SerializeField] private bool saveAndLoadData;

    public bool SaveAndLoadData => saveAndLoadData;
}