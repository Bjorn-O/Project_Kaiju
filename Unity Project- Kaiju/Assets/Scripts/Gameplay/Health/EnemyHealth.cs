using UnityEngine;


[RequireComponent(typeof(HealthEvents))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private uint maxHealth = 100;
    [SerializeField] private uint currentHealth;

    public HealthEvents healthEvents;


    private void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(uint damage)
    {
        currentHealth -= damage;
        healthEvents.TriggerHealthChanged(currentHealth);
        print("enemy health: " + currentHealth);
        if (currentHealth <= 0)
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        Debug.Log("Enemy has died.");
        healthEvents.TriggerHealthZero();
    }


}