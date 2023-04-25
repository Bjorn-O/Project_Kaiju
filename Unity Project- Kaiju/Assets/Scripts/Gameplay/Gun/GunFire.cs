using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class GunFire : MonoBehaviour
{
    [FormerlySerializedAs("shootAble")] [SerializeField] private LayerMask shootAbleLayer;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private VisualEffect shootEffect;
    
    [SerializeField] private float fireRatePerSecond;

    private RaycastHit _hit;
    private int i;

    private void OnFire(InputValue value)
    {
        var isPressed = value.Get<float>();

        if (isPressed > 0)
        {
            InvokeRepeating(nameof(Pew), 0, (60 / fireRatePerSecond) / 100);
            
            print(60 / fireRatePerSecond / 100);
        }
        else CancelInvoke(nameof(Pew));
        

    }

    private void Pew()
    {
        shootEffect.Play();
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out _hit, shootAbleLayer))
        {
            
        }
        //Play Sound effect
        //Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward, Color.red);
    }
}
