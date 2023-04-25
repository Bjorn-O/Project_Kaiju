using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthTest : MonoBehaviour
{
    [SerializeField] private uint damage;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private PlayerHealth playerHealth;


    public void TestDamage()
    {
        enemyHealth.TakeDamage(damage);
    }

    public void TestPlayerDamage()
    {
        playerHealth.PlayerDamage(damage);  
    }
}
