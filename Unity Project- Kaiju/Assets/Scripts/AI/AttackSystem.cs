using System.Collections;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private GameObject floorMarkerObject;
    [SerializeField] private Vector3 storePosition;
    [SerializeField] private GameObject boatObject;
    private Quaternion rotation;
    private CapsuleCollider damageTrigger;
    private SpriteRenderer markerRenderer;

    private void Awake()
    {
        damageTrigger = floorMarkerObject.GetComponent<CapsuleCollider>();
        markerRenderer = floorMarkerObject.GetComponent<SpriteRenderer>();
        rotation = Quaternion.Euler(90, 0, 0);
    }
    
    public IEnumerator FloorMarkerAttack(Vector3 position, float timeToWait, float yScale = default)
    {
        floorMarkerObject.transform.position = position;
        floorMarkerObject.transform.rotation = rotation;
        yield return new WaitForSeconds(timeToWait);
        markerRenderer.enabled = false;

        if (Vector3.Distance(boatObject.transform.position, damageTrigger.transform.position) < damageTrigger.radius) //Checks if boat is in trigger radius
        {
            //Damage function
        }

        floorMarkerObject.transform.position = storePosition;
        markerRenderer.enabled = true;
    }
}
