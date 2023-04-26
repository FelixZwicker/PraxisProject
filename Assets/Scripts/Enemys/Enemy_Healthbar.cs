using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Healthbar : MonoBehaviour
{
    public Slider Slider;
    public Vector3 offset;

    public void SetEnemyHealthbar(int maxHealth, int currentHealth)
    {
        Slider.gameObject.SetActive(currentHealth < maxHealth);
        Slider.maxValue = maxHealth;
        Slider.value = currentHealth;
    }

    void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
