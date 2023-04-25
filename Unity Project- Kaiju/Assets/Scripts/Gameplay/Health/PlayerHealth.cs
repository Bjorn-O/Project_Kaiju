using UnityEngine;


[RequireComponent(typeof(HealthEvents))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private uint maxHealth = 100;
    [SerializeField] private uint currentHealth;

    public HealthEvents healthEvents;


    private void Start()
    {
        currentHealth = maxHealth;
    }


    public void PlayerDamage(uint Damage)
    {
        currentHealth -= Damage;
        healthEvents.TriggerHealthChanged(currentHealth);
        if (currentHealth <= 0)
        {
            PlayerDeath();  
        }
    }

    private void PlayerDeath()
    {
        healthEvents.TriggerHealthZero();
    }

}
