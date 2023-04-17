using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private GameObject floorMarkerSprite;
    private Quaternion rotationQuaternion;

    private void Awake()
    {
        rotationQuaternion = Quaternion.Euler(90, 0, 0);
    }
    
    public IEnumerator CircleAttack(Vector3 position, Quaternion rotation, float timeToWait)
    {
        Instantiate(floorMarkerSprite, position, rotation);
        yield return new WaitForSeconds(timeToWait);

    }
}
