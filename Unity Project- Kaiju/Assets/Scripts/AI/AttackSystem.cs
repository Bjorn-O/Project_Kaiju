using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private GameObject circleFloorMarkerSprite;
    [SerializeField] private Vector3 storePosition;
    [SerializeField] private GameObject boatObject;
    private CapsuleCollider damageTrigger;
    private SpriteRenderer circleSprite;

    private void Awake()
    {
        damageTrigger = circleFloorMarkerSprite.GetComponent<CapsuleCollider>();
        circleSprite = circleFloorMarkerSprite.GetComponent<SpriteRenderer>();
    }
    
    public IEnumerator FloorMarkerAttack(Vector3 position, Quaternion rotation, float timeToWait)
    {
        circleFloorMarkerSprite.transform.position = position;
        circleFloorMarkerSprite.transform.rotation = rotation;
        yield return new WaitForSeconds(timeToWait);
        circleSprite.enabled = false;
        if (Vector3.Distance(boatObject.transform.position, damageTrigger.transform.position) < damageTrigger.radius) //Checks if boat is in trigger radius
        {
            Debug.Log("Hit!");
            //Damage function
        }
        circleFloorMarkerSprite.transform.position = storePosition;
        circleSprite.enabled = true;
    }
}
