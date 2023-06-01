using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float EnemyHealth;


    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
