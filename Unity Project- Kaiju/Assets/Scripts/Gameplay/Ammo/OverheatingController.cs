using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OverheatingController : MonoBehaviour
{
   [SerializeField] private float heatThreshold; // Maximum heat before gun overheats
   [SerializeField] private float heatIncreaseRate; // Heat increase per second while firing
   [SerializeField] private float heatDecreaseRate; // Heat decrease per second while not firing
   [SerializeField] private float cooldownDelay; // Time in seconds before gun starts cooling down after overheating
    
    public UnityEvent OnOverheated; // Event to invoke when the gun overheats
    public UnityEvent OnCooledDown; // Event to invoke when the gun cools down
    
    private float _currentHeat; // Current heat of the gun
    private bool _isOverheated = false; // Flag to track if the gun is overheated
    

    // fire() and cooldown() need to be called from the actual shooting script
    
    private IEnumerator CoolingDown()
    {
        yield return new WaitForSeconds(cooldownDelay);
        _isOverheated = false;
        OnCooledDown.Invoke();
    }
    
    public void Fire()
    {
        _currentHeat += heatIncreaseRate * Time.deltaTime;
        
        if (_currentHeat >= heatThreshold)
        {
            _isOverheated = true;
            _currentHeat = heatThreshold;
            OnOverheated.Invoke();
            StartCoroutine(CoolingDown());
        }
    }
    
    public void CoolDown()
    {
        _currentHeat -= heatDecreaseRate * Time.deltaTime;
        _currentHeat = Mathf.Clamp(_currentHeat, 0f, heatThreshold);
        if (_isOverheated && _currentHeat == 0f)
        {
            _isOverheated = false;
            OnCooledDown.Invoke();
        }
    }
    

}