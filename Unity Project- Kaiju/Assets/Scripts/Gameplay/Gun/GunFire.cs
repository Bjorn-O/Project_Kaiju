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

    [SerializeField] private float damagePerShot;
    [SerializeField] private float fireRatePerSecond;

    AudioManager audioManager;

    private RaycastHit _hit;


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
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out _hit, Mathf.Infinity, shootAbleLayer))
        {
            var enemy = _hit.transform.GetComponentInParent<EnemyHealth>();
            if(enemy != null && enemy.CompareTag("Enemy"))
            {
                enemy.TakeDamage(damagePerShot);
                hitMarker.Hit();
                print("HIT!");
            }
        }
        recoil.RecoilOnFire();
        audioManager.Play("Shoot");
    }
}
