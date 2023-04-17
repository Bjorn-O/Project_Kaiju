using UnityEngine;

public class HealthEventsTest : MonoBehaviour
{
    public HealthEvents healthEvents;

    void OnEnable()
    {
        healthEvents.onHealthChanged.AddListener(OnHealthChanged);
        healthEvents.onHealthZero.AddListener(OnHealthZero);
    }

    void OnDisable()
    {
        healthEvents.onHealthChanged.RemoveListener(OnHealthChanged);
        healthEvents.onHealthZero.RemoveListener(OnHealthZero);
    }

    public void OnHealthChanged(uint health)
    {
        Debug.Log("Health changed to: " + health);
    }

    public void OnHealthZero()
    {
        Debug.Log("Health reached zero!");
    }
}