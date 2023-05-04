using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private GameObject rayOrigin;
    private Vector3 rayDirection;
    private Sprite crosshairSprite;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rayDirection = this.transform.position - rayOrigin.transform.position;

        Physics.Raycast(rayOrigin.transform.position, transform.forward, 50);
        Debug.DrawRay(rayOrigin.transform.position, transform.forward);
    }
}
