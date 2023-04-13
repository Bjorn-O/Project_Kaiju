using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private uint maxHealth = 100;
    [SerializeField] private uint currentHealth;
    [SerializeField] private Slider healthSlider;

    public HealthEvents healthEvents;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(uint damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        healthEvents.TriggerHealthChanged(currentHealth);

    }

}
