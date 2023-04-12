using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    private PlayerControls PlayerControls;
    
    [Header("Bullet settings")]
    [SerializeField] private Transform gun;
    [SerializeField] private float firingDelay; //Delay between shots in seconds
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private bool canShoot;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Clamp values")]
    [SerializeField] private int rotationMinX;
    [SerializeField] private int rotationMaxX;
    [SerializeField] private int rotationMinY;
    [SerializeField] private int rotationMaxY;
    [SerializeField] private int rotationMaxZ;
    [SerializeField] private int rotationMinZ;

    [SerializeField] private float turningSpeed;
    private float firingTimer; //Timer used to time between shots
    private Rigidbody bulletRigidbody;
    private Vector3 aimPosition;

    private void Awake()
    {
        PlayerControls = new PlayerControls();
        canShoot = true;
        firingTimer = 0;
    }

    private void OnEnable()
    {
        PlayerControls.Enable();
    }

    private void OnDisable()
    {
        PlayerControls.Disable();
    }

    private void Update()
    {
        if (!canShoot)
        {
            firingTimer += Time.deltaTime;

            if (firingTimer > firingDelay)
            {
                firingTimer = 0;
                canShoot = true;
            }
        }

        Aiming();
        
        // Check for fire input
        if (PlayerControls.Turret.shooting.triggered && canShoot)
        {
            Fire();
        }

    }

    void Aiming()
    {
        aimPosition = Camera.main.ScreenPointToRay(PlayerControls.Turret.aiming.ReadValue<Vector2>()).GetPoint(10);

        // Clamp the rotation values to the specified limits
        aimPosition = new Vector3(
           Mathf.Clamp(aimPosition.x, rotationMinX, rotationMaxX),
           Mathf.Clamp(aimPosition.y, rotationMinY, rotationMaxY),
           Mathf.Clamp(aimPosition.z, rotationMinZ, rotationMaxZ));

        //gun.transform.rotation = Quaternion.Euler(targetRotation.y, -targetRotation.x, 0f);
        gun.transform.LookAt(aimPosition);
    }


    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector3 bulletDirection = bulletSpawnPoint.forward;
        Vector3 bulletVelocity = bulletDirection * bulletSpeed;
        bulletVelocity.y -= gravity * Time.deltaTime;
        bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = bulletVelocity;

        canShoot = false;
    }

    
}

