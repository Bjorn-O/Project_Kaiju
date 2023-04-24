using UnityEngine;

public class EnemyattackTest : MonoBehaviour
{
    [SerializeField] private uint Damage;

    public PlayerHealth playerHealth;


    public void HitPlayer()
    {
        playerHealth.PlayerDamage(Damage);
    }

}
