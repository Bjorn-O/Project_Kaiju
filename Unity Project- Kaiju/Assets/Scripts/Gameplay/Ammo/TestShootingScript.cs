
using UnityEngine;

public class TestShootingScript : MonoBehaviour
{
    [SerializeField] private AmmoCount ammoCount;

    void Update()
    {
        // check for fire input
        if (Input.GetButtonDown("Fire1") && ammoCount.currentAmmo > 0 && ammoCount.canRefill == true)
        {
            // fire bullet and decrement ammo count
            ammoCount.currentAmmo--;
            ammoCount.UpdateUI();
        }
    }


}
