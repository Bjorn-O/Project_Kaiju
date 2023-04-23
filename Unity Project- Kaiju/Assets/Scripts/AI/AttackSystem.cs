using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private GameObject floorMarkerSprite;
    [SerializeField] private GameObject boatObject;
    [SerializeField] private CapsuleCollider damageTrigger;
    private Quaternion rotationQuaternion;

    private void Awake()
    {
        rotationQuaternion = Quaternion.Euler(90, 0, 0);
    }
    
    public IEnumerator FloorMarkerAttack(Vector3 position, Quaternion rotation, float timeToWait)
    {
        Instantiate(floorMarkerSprite, position, rotation);
        yield return new WaitForSeconds(timeToWait);
        if (Vector3.Distance(boatObject.transform.position, damageTrigger.transform.position) > damageTrigger.radius)
        {
            //Damage function
        }
    }
}
