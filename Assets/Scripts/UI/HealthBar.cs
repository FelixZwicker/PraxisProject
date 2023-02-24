using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthDisplay;

    public void SetHealth(int health, int maxHealth)
    {
        slider.maxValue = health;
        slider.value = health;
        healthDisplay.text = health.ToString() + " / " + maxHealth.ToString();
    }


}
