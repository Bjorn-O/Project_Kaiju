using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OverheatingController : MonoBehaviour
{
   [SerializeField] private float heatThreshold = 80f; // Maximum heat before gun overheats
   [SerializeField] private float heatIncreaseRate = 20f; // Heat increase per second while firing
   [SerializeField] private float heatDecreaseRate = 30f; // Heat decrease per second while not firing
   [SerializeField] private float cooldownDelay = 2f; // Time in seconds before gun starts cooling down after overheating
    
    public UnityEvent OnOverheated; // Event to invoke when the gun overheats
    public UnityEvent OnCooledDown; // Event to invoke when the gun cools down
    
    private float currentHeat = 0f; // Current heat of the gun
    public bool isOverheated = false; // Flag to track if the gun is overheated
    
    private void Update()
    {

        // we need to get the actual firing from the aiming and shooting system this is just for testing
/*
        if (Input.GetMouseButton(0) && !isOverheated)
        {
            Fire();
            print("firing");
        }
        else
        {
            CoolDown();
            print("cooling");
        }
*/
    }
    
    private IEnumerator CoolingDown()
    {
        yield return new WaitForSeconds(cooldownDelay);
        isOverheated = false;
        OnCooledDown.Invoke();
        print("enumcooling");
    }
    
    public void Fire()
    {
        currentHeat += heatIncreaseRate * Time.deltaTime;
        
        if (currentHeat >= heatThreshold)
        {
            print("isoverheated");
            isOverheated = true;
            currentHeat = heatThreshold;
            OnOverheated.Invoke();
            StartCoroutine(CoolingDown());
            print("cooling1");
        }
    }
    
    public void CoolDown()
    {
        currentHeat -= heatDecreaseRate * Time.deltaTime;
        currentHeat = Mathf.Clamp(currentHeat, 0f, heatThreshold);
        print("func cooldown");
        if (isOverheated && currentHeat <= 0f)
        {
            isOverheated = false;
            OnCooledDown.Invoke();
        }
    }
    

}