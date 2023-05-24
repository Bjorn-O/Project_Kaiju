using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class GunFire : MonoBehaviour
{
    [SerializeField] private Hitmarker hitMarker;
    [SerializeField] private Recoil recoil;

    [SerializeField] private LayerMask shootAbleLayer;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private VisualEffect shootEffect;
    
    [SerializeField] private float fireRatePerSecond;

    AudioManager audioManager;

    private RaycastHit _hit;
    private int i;


    private void OnFire(InputValue value)
    {
        var isPressed = value.Get<float>();

        if (isPressed > 0)
        {
            InvokeRepeating(nameof(Pew), 0, (60 / fireRatePerSecond) / 100);
        }
        else CancelInvoke(nameof(Pew));
    }
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Pew()
    {
        shootEffect.Play();
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out _hit, shootAbleLayer))
        {
            //Debug.Log(_hit.transform.name);

            var enemy = _hit.transform.GetComponentInParent<EnemyHealth>();
            if(enemy != null && enemy.CompareTag("EnemyMain"))
            {
                enemy.TakeDamage(50);
                hitMarker.Hit();
            }           
            else if (enemy != null && enemy.CompareTag("Enemy"))
            {
                enemy.TakeDamage(33);
                hitMarker.Hit();
            }

        }
        recoil.RecoilOnFire();
        audioManager.Play("Shoot");
        //Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward, Color.red);
    }
}
