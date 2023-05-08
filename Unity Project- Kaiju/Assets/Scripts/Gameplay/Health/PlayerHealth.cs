using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamage
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;

    public UnityEvent OnDamage;
    public UnityEvent OnDeath;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
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
