using UnityEngine;
using UnityEngine.UI;

public class ProgressBarHandler : MonoBehaviour
{
    public Slider slider;

    public void SetMaxValue(int wagons)
    {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(64 * (wagons + 1), rectTransform.sizeDelta.y);
        slider.maxValue = wagons;
        slider.value = 0;
    }

    public void SetValue(int value)
    {
        slider.value = value;
    }
}
