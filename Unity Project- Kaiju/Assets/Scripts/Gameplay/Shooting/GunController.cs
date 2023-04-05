using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    private PlayerControls PlayerControls;
    
    [SerializeField] private Transform gun;
    [SerializeField] private float horizontalRotationSpeed = 5f;
    [SerializeField] private float verticalRotationSpeed = 5f;
    [SerializeField] private float horizontalRotationLimit = 45f;
    [SerializeField] private float verticalRotationLimit = 45f;
    [SerializeField] private float resetSpeed = 2f;
    [SerializeField] private float firingDelay; //Delay between shots in seconds
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private bool canShoot;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    private float firingTimer; //Timer used to time between shots
    private Rigidbody bulletRigidbody;


    private Vector2 rotationInput;
    private Vector3 targetRotation;

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
            Debug.Log(firingTimer);

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
        rotationInput = PlayerControls.Turret.aiming.ReadValue<Vector2>();

        // Calculate the target rotation
        targetRotation.y += rotationInput.x * horizontalRotationSpeed;
        targetRotation.x += rotationInput.y * verticalRotationSpeed;

        // Clamp the rotation values to the specified limits
        targetRotation.y = Mathf.Clamp(targetRotation.y, -horizontalRotationLimit, horizontalRotationLimit);
        targetRotation.x = Mathf.Clamp(targetRotation.x, -verticalRotationLimit, verticalRotationLimit);

        // Smoothly rotate the gun towards the target rotation
        gun.localRotation = Quaternion.Slerp(gun.localRotation, Quaternion.Euler(targetRotation), Time.deltaTime * resetSpeed);

    }


    private void Fire()
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    
        // Calculate the direction to shoot the bullet
        Vector3 bulletDirection = bulletSpawnPoint.forward;
    
        // Apply the initial bullet speed
        Vector3 bulletVelocity = bulletDirection * bulletSpeed;
    
        // Apply gravity to the bullet
        bulletVelocity.y -= gravity * Time.deltaTime;

        // Apply the velocity to the bullet's rigidbody
        bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = bulletVelocity;

        canShoot = false;
    }

    
}

