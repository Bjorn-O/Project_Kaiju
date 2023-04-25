using UnityEngine;
using UnityEngine.Events;


public class EnemyHealth : MonoBehaviour, IDamage
{
    [SerializeField] private uint maxHealth = 200;
    [SerializeField] private uint currentHealth;

    public UnityEvent OnDamage;
    public UnityEvent OnDeath;


    private void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(uint damage)
    {
        currentHealth -= damage;
        OnDamage.Invoke();
        if (currentHealth <= 0)
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        OnDeath.Invoke();   
    }

}