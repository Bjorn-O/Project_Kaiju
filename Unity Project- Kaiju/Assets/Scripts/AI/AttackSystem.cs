using System.Collections;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private GameObject floorMarkerObject;
    [SerializeField] private Vector3 storePosition;
    [SerializeField] private GameObject boatObject;
    [SerializeField] private Sprite[] floorMarkers;
    [SerializeField] private MeshCollider[] attackColliders;

    private MeshCollider usedCollider;
    private Quaternion rotation;
    private SpriteRenderer markerRenderer;

    private void Awake()
    {
        markerRenderer = floorMarkerObject.GetComponent<SpriteRenderer>();
        rotation = Quaternion.Euler(90, 0, 0);
    }

    private void Start()
    {
        StartCoroutine(FloorMarkerAttack(new Vector3(0, 10, 10), boatObject.transform.position, 5, 1));
    }


    /// <summary>
    /// Creates floor marker and checks if players are inside it.
    /// </summary>
    /// <param name="spawnPosition">Spawn position of floor marker.</param>
    /// <param name="objectToFace">Which object does it face.</param>
    /// <param name="timeToWait">Time before damage is applied.</param>
    /// <param name="sprite">Sprite and corresponding collider to use on floor marker.</param>
    public IEnumerator FloorMarkerAttack(Vector3 spawnPosition, Vector3 objectToFace, float timeToWait, int sprite)
    {
        markerRenderer.sprite = floorMarkers[sprite];
        usedCollider = attackColliders[sprite];
        floorMarkerObject.transform.position = spawnPosition;

        objectToFace.x = 90;
        objectToFace.z = 0;

        floorMarkerObject.transform.LookAt(objectToFace);

        yield return new WaitForSeconds(timeToWait);
        markerRenderer.enabled = false;

        if (usedCollider.bounds.Contains(boatObject.transform.position)) //Checks if boat is in trigger radius
        {
            Debug.Log("Hit");
            //Damage function
        }

        floorMarkerObject.transform.position = storePosition;
        markerRenderer.enabled = true;
    }
}
