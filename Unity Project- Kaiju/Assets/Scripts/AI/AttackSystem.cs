using System.Collections;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private GameObject floorMarkerObject;
    [SerializeField] private Vector3 storePosition;
    [SerializeField] private GameObject boatObject;
    [SerializeField] private Sprite[] floorMarkers;
    [SerializeField] private MeshCollider[] attackColliders;

    private bool isCancelled;
    private MeshCollider usedCollider;
    private SpriteRenderer markerRenderer;

    public bool IsCancelled { get; set; }

    private void Awake()
    {
        isCancelled = false;
        markerRenderer = floorMarkerObject.GetComponent<SpriteRenderer>();
    }


    /// <summary>
    /// Creates floor marker and checks if players are inside it.
    /// </summary>
    /// <param name="spawnPosition">Spawn position of floor marker.</param>
    /// <param name="objectToFace">Which object does it face.</param>
    /// <param name="timeToWait">Time before damage is applied.</param>
    /// <param name="sprite">Sprite and corresponding collider to use on floor marker.</param>
    public IEnumerator FloorMarkerAttack(Vector3 spawnPosition, float timeToWait, int sprite)
    {
        markerRenderer.sprite = floorMarkers[sprite];
        usedCollider = attackColliders[sprite];
        floorMarkerObject.transform.position = spawnPosition;

        yield return new WaitForSeconds(timeToWait);
        markerRenderer.enabled = false;

        if (isCancelled)
        {
            isCancelled = false;
        }
        else 
        {
            if (usedCollider.bounds.Contains(boatObject.transform.position)) //Checks if boat is in trigger radius
            {
                Debug.Log("Hit");
                //Damage function
            }
        }

        floorMarkerObject.transform.position = storePosition;
        markerRenderer.enabled = true;
    }
}
