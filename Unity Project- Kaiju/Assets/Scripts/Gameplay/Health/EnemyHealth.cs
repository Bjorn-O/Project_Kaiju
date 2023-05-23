using UnityEngine;
using UnityEngine.Events;


public class EnemyHealth : MonoBehaviour, IDamage
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    public UnityEvent<float> OnDamage;
    public UnityEvent OnDeath;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        OnDamage.Invoke(damage);
        if (currentHealth <= 0)
        {
            EnemyDeath();
        }
    }
    
    public void EnemyDeath()
    {
        OnDeath.Invoke();
        print("DEATH INVOKED");
        Destroy(gameObject);
    }

}