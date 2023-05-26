using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    //public Transform[] waypoints;
    public List<GameObject> waypoints =  new List<GameObject>(); 

    void Awake()
    {
        foreach (Transform tr in GetComponentsInChildren<Transform>().Skip(1))
        {
            waypoints.Add(tr.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for(int i = 0; i < transform.childCount -1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
       Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }
}
