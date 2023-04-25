using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamage
{
    [SerializeField] private uint maxHealth = 100;
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
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        OnDamage.Invoke();
    }

}
