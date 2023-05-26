using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTotalHealh : MonoBehaviour
{
    public Healthbar healthbar;
    AudioManager audioManager;

    public EnemyHealth headHealth;
    private EnemyHealth health;

    private float _totalMaxHealth;
    private float _totalCurrentHealth;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                _totalMaxHealth += health.GetMaxHealth();
                health.OnDamage.AddListener(TakeTotalDamage);
            }
        }

        _totalMaxHealth += 600;
        headHealth.SetMaxHealth(_totalMaxHealth);

        headHealth.OnDamage.AddListener(TakeTotalDamage);
        _totalCurrentHealth = _totalMaxHealth;
        healthbar.SetMaxHealth(_totalMaxHealth);

        Debug.Log("Total enemy health: " + _totalMaxHealth);
        Debug.Log("Current enemy health: " + _totalCurrentHealth); 
    }

    public void TakeTotalDamage(float damage)
    {
        _totalCurrentHealth -= damage;
        healthbar.SetHealth(_totalCurrentHealth);

        if (_totalCurrentHealth <= 0)
        {
            //Kills Head
            headHealth.EnemyDeath();
            audioManager.Play("MonsterDeath");
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
