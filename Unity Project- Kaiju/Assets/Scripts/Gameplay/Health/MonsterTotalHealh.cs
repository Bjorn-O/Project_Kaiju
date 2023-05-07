using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTotalHealh : MonoBehaviour
{
    public Healthbar healthbar;

    public EnemyHealth headHealth;
    private EnemyHealth health;

    private uint totalMaxHealth;
    private uint totalCurrentHealth;

    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                totalMaxHealth += health.GetMaxHealth();
                health.OnDamage.AddListener(TakeTotalDamage);
            }
        }

        totalMaxHealth += 600;
        headHealth.SetMaxHealth(totalMaxHealth);

        headHealth.OnDamage.AddListener(TakeTotalDamage);
        totalCurrentHealth = totalMaxHealth;
        healthbar.SetMaxHealth(totalMaxHealth);

        Debug.Log("Total enemy health: " + totalMaxHealth);
        Debug.Log("Current enemy health: " + totalCurrentHealth); 
    }

    public void TakeTotalDamage(uint damage)
    {
        totalCurrentHealth -= damage;
        healthbar.SetHealth(totalCurrentHealth);

        if (totalCurrentHealth <= 0)
        {
            //Kills Head
            headHealth.EnemyDeath();
            //Kills Tentacles
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                health = enemy.GetComponent<EnemyHealth>();
                health.EnemyDeath();
            }
        }
    }
}
