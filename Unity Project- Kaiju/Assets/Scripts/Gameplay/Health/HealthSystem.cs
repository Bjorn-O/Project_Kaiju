using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth; // Set the current health to the max health when the game starts
    }

    public void TakeMinorDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void TakeMediumDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void TakeMajorDamage(int damage)
    {
        currentHealth -= damage;
    }

}