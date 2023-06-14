using Sirenix.OdinInspector;
using UnityEngine;

    
[CreateAssetMenu(menuName = "Config/Create OdinTempClass", fileName = "OdinTempClass", order = 0)]
public class OdinTempClass : ScriptableObject
{
    [ShowInInspector]
    [TableMatrix]
    public int[,] CellContentMatrix = new int[4,4];
}
