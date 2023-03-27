using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightScript : MonoBehaviour
{
    [SerializeField] private GameObject spotlight;
    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(10);
        spotlight.transform.LookAt(mousePosition);
    }
}
