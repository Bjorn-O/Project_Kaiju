using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    [SerializeField] private float refillDelay = 2f; // time delay before ammo refill in seconds
    [SerializeField] private int maxAmmo = 10; // maximum ammo count
    public int currentAmmo = 10; // current ammo count
    public bool canRefill = true; // flag to control ammo refill timing


    [SerializeField] private TMP_Text ammoCount;

    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateUI(); 
    }

    public void UpdateUI()
    {
        ammoCount.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();

    }

    // function to add ammo with a timed delay
    public void AddAmmo(int amount)
    {
        if (canRefill)
        {
            canRefill = false;
            currentAmmo = maxAmmo;
            StartCoroutine(RefillDelay());
        }
    }

    // coroutine to delay ammo refill
    private IEnumerator RefillDelay()
    {
        yield return new WaitForSeconds(refillDelay);
        canRefill = true;
        UpdateUI();
    }


}
