using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunFire : MonoBehaviour
{
    [SerializeField] private float fireRate;

    private void OnFire(InputValue value)
    {
        print(value.Get<float>());
    }
}
