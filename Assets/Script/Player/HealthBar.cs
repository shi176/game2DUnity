using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI valueText;

    public void UpdateBar(int minValue, int maxvalue)
    {
        fillBar.fillAmount = (float)minValue / (float)maxvalue;
        if (valueText)
        {
            valueText.text = minValue.ToString() + " / " + maxvalue.ToString();
        }
    }

}
