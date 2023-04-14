using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    private PlayerControls PlayerControls;
    
    [Header("Gun settings")]
    [SerializeField] private Transform gun;
    [SerializeField] private float firingDelay; //Delay between shots in seconds
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private bool canShoot;
    [SerializeField] private int rayDistance;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Clamp values")]
    [SerializeField] private int rotationMinY;
    [SerializeField] private int rotationMaxY;
    [SerializeField] private int rotationMinX;
    [SerializeField] private int rotationMaxX;

    [SerializeField] private float turningSpeed;
    private float _firingTimer; //Timer used to time between shots
    private Rigidbody _bulletRigidbody;
    private Vector3 _rotationInput;
    private Vector3 _targetRotation;

    private void Awake()
    {
        PlayerControls = new PlayerControls();
        canShoot = true;
        _firingTimer = 0;
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
            _firingTimer += Time.deltaTime;

            if (_firingTimer > firingDelay)
            {
                _firingTimer = 0;
                canShoot = true;
            }
        }
        // Should be an event
        Aiming();
        
        // Should be an event
        if (PlayerControls.Turret.shooting.triggered && canShoot)
        {
            Fire();
        }

    }

    void Aiming()
    {
        //Camera.main is an expensive/slow function. Readvalue is an expensive/slow function. Both in Update.
        
        // rotationInput = Camera.main.ScreenPointToRay(PlayerControls.Turret.aiming.ReadValue<Vector2>()).GetPoint(rayDistance);

        
        //Creating a new Vector 3 every frame, expensive
        _targetRotation = new Vector3(
          Mathf.Clamp(_rotationInput.x, rotationMinX, rotationMaxX),
          Mathf.Clamp(_rotationInput.y, -rotationMaxY, -rotationMinY), //Y rotation uses negative value, Max and Min should be reversed and negative
          _rotationInput.z);

        //Hard numbers. Why times 10?
        gun.transform.rotation = Quaternion.Euler(-_targetRotation.y * 10, _targetRotation.x * 10, 0f);
    }
    
    private void Fire()
    {
        // Should be Raycast
        
        // GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        // Vector3 bulletDirection = bulletSpawnPoint.forward;
        // Vector3 bulletVelocity = bulletDirection * bulletSpeed;
        // bulletVelocity.y -= gravity * Time.deltaTime;
        
        // GetComponent every frame. Great.
        
        // bulletRigidbody = bullet.GetComponent<Rigidbody>();
        // bulletRigidbody.velocity = bulletVelocity;

        canShoot = false;
    }
}