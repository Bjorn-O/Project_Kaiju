using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamage
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth;

    public UnityEvent OnDamage;
    public UnityEvent OnDeath;


    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        OnDamage.Invoke();
        if (_currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        OnDamage.Invoke();
    }

}
