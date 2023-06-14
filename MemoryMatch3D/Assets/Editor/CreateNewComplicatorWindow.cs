using System;
using Complicators;
using UnityEngine;
#if UNITY_EDITOR

    using Sirenix.OdinInspector;
    using Sirenix.OdinInspector.Editor;
    using UnityEditor;

#endif

public class CreateNewComplicatorWindow : OdinEditorWindow
{
    [MenuItem("Tools/LevelEditor/Create New Complicator...")]
    public static void OpenWindow()
    {
        GetWindow<CreateNewComplicatorWindow>().Show();
    }
    [OnValueChanged("CheckDirectionDescription")]
    public ComplicatorType instanceType;

    [HideIf("instanceType", ComplicatorType.DEFAULT)]
    [HorizontalGroup("random")]
    public bool randomizeTarget = false;
    
    #region shift rows or columns
    [ShowIf("@this.instanceType == ComplicatorType.SHIFT_ROW || this.instanceType == ComplicatorType.SHIFT_COLUMN")]
    [LabelText("@GetTargetLabel")]
    [DisableIf("randomizeTarget")]
    [HorizontalGroup("random")]
    public int targetIndex;
    
    [ShowIf("@this.instanceType == ComplicatorType.SHIFT_ROW || this.instanceType == ComplicatorType.SHIFT_COLUMN")]
    private string _defaultDirectionInfo;
    
    [ShowIf("@this.instanceType == ComplicatorType.SHIFT_ROW || this.instanceType == ComplicatorType.SHIFT_COLUMN")]    
    [InfoBox(@"@""Default direction is "" + this._defaultDirectionInfo")]
    public bool isDirectionDefault = true;

    #endregion

    #region switch targets
    [ShowIf("instanceType", ComplicatorType.SWITCH_TWO_POKEBALLS)]
    [DisableIf("randomizeTarget")]
    public Vector2Int[] targets;
    #endregion
    
    [HideIf("instanceType", ComplicatorType.DEFAULT)]
    public bool repeatable = false;
    
    [HideIf("instanceType", ComplicatorType.DEFAULT)]
    [EnableIf("repeatable"), Range(0,10)]//todo maximum value?
    public uint repeatFrequency;

    private void HandelRepeatFrequency(ref uint repeatFrequency, ref bool repeatable)
    {
        var levelEditorWindow = GetWindow<LevelEditorWindow>(false, "Level Editor", false);
        levelEditorWindow.RepeatFrequency = repeatFrequency;
        if(repeatFrequency == 0)
        {
            levelEditorWindow.Repeatable = false;
            repeatable = false;
            return;
        }
        levelEditorWindow.Repeatable = true;
    }

    private void SetComplicatorToEditorWindow(ref ComplicatorData complicator)
    {
        var levelEditorWindow = GetWindow<LevelEditorWindow>(false, "Level Editor", false);
        levelEditorWindow.LastCreatedComplicator = complicator;
    }
    
    private void CheckDirectionDescription()
    {
        var label = instanceType == ComplicatorType.SHIFT_COLUMN ? "Target Column" : "Target Row";
        _defaultDirectionInfo = instanceType switch
        {
            ComplicatorType.SHIFT_COLUMN => "Up to Down",
            ComplicatorType.SHIFT_ROW => "Left to Right",
            _ => null
        };
    }
    private string GetTargetLabel => instanceType == ComplicatorType.SHIFT_COLUMN ? "Target Column" : "Target Row";
    [HideIf("instanceType", ComplicatorType.DEFAULT)]
    [Button(ButtonSizes.Large), GUIColor(0, 0.8f, 0)]
    private void ExportComplicator()
    {
        var complicatorName = $"CR_" + instanceType.GetType().ToString();
        var baseFilename = $"Assets/Editor/LevelEditor/ComplicatorsInstances/{complicatorName}.asset";
        var newFilename = AssetDatabase.GenerateUniqueAssetPath(baseFilename);
        ComplicatorData obj = null;
        HandelRepeatFrequency(ref repeatFrequency, ref  repeatable);
        switch (instanceType)
        {
            case ComplicatorType.DEFAULT:
                break;
            case ComplicatorType.SHIFT_COLUMN:
                obj = ScriptableObject.CreateInstance<ShiftColumnComplicator>();
                ((ShiftColumnComplicator)obj).IsDirectionDefault = isDirectionDefault;
                ((ShiftColumnComplicator)obj).TargetColumnIndex = targetIndex;
                ((ShiftColumnComplicator)obj).RandomizeTarget = randomizeTarget;
                AssetDatabase.CreateAsset(((ShiftColumnComplicator)obj),newFilename);
                AssetDatabase.SaveAssets();
                break;
            case ComplicatorType.SHIFT_ROW:
                obj = CreateInstance<ShiftRowComplicator>();
                ((ShiftRowComplicator)obj).IsDirectionDefault = isDirectionDefault;
                ((ShiftRowComplicator)obj).TargetRowIndex = targetIndex;
                ((ShiftRowComplicator)obj).RandomizeTarget = randomizeTarget;
                AssetDatabase.CreateAsset(((ShiftRowComplicator)obj),newFilename);
                AssetDatabase.SaveAssets();
                break;
            case ComplicatorType.SWITCH_TWO_POKEBALLS:
                if(targets.Length < 2 ) return;
                obj = CreateInstance<SwitchPokeballsComplicator>();
                ((SwitchPokeballsComplicator)obj).TargetACellIndex = targets[0];
                ((SwitchPokeballsComplicator)obj).TargetBCellIndex = targets[1];
                ((SwitchPokeballsComplicator)obj).RandomizeTarget = randomizeTarget;
                AssetDatabase.SaveAssets();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        SetComplicatorToEditorWindow(ref obj);
        obj.IsRepeatable = repeatable;
        obj.RepeatFrequency = repeatFrequency;
        this.Close();
        // return null;
    }
}
