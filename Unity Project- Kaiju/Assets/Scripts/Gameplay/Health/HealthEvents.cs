using UnityEngine;
using UnityEngine.Events;

public class HealthEvents : MonoBehaviour
{
    [System.Serializable]
    public class HealthChangeEvent : UnityEvent<uint> { }

    [System.Serializable]
    public class HealthZeroEvent : UnityEvent { }

    public HealthChangeEvent onHealthChanged;
    public HealthZeroEvent onHealthZero;

    public void TriggerHealthChanged(uint health)
    {
        onHealthChanged.Invoke(health);
    }

    public void TriggerHealthZero()
    {
        onHealthZero.Invoke();
    }
}