using UnityEngine;
using UnityEngine.Events;


public class EnemyHealth : MonoBehaviour, IDamage
{
    [SerializeField] private uint maxHealth;
    [SerializeField] private uint currentHealth;

    public UnityEvent<uint> OnDamage;
    public UnityEvent OnDeath;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public uint GetMaxHealth()
    {
        return maxHealth; 
    }

    public void SetMaxHealth(uint health)
    {
        maxHealth = health;
    }

    public void TestDMG()
    {
        TakeDamage(100);
    }

    public void TakeDamage(uint damage)
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
        Destroy(gameObject);
    }

}