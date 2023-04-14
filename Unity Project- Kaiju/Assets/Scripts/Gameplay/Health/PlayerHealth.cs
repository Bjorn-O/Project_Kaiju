using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthEvents))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private uint maxHealth = 100;
    [SerializeField] private uint currentHealth;
    [SerializeField] private Slider healthSlider;

    public HealthEvents healthEvents;


    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
    }


    public void PlayerDamage()
    {
        healthEvents.TriggerHealthChanged(currentHealth);
    }

}
