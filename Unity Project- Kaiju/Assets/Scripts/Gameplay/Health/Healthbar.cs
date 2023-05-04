using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(uint health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(uint health)
    {
        slider.value = health;
    }
}
