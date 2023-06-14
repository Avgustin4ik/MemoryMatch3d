using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens.Meta
{
    [RequireComponent(typeof(Slider))]
    internal class ProgressSlider : MonoBehaviour
    {
        [field: SerializeField]  public Slider SliderElement { get; private set; }
        [field: SerializeField]  public TMPro.TMP_Text ProgressLabel { get; private set; }
        
    }
}