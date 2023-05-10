using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FovOnAccelerate : MonoBehaviour
{
    Camera playerCamera;

    float standardFOV = 60f;
    float currentFOV;
    float speedFOV = 80f;
    public bool accelerate = false;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponent<Camera>();
        currentFOV = standardFOV;
    }

    private void Update()
    {
        if(accelerate) HigherFOV(currentFOV);
        else if(!accelerate) LowerFOV(currentFOV);
    }

    public void HigherFOV(float s)
    {
        float t = 0f;
        float minimum = s;
        float maximum = speedFOV;

        t += 1f * Time.deltaTime;
        currentFOV = Mathf.Lerp(minimum, maximum, t);

        playerCamera.fieldOfView = currentFOV;
    }

    public void LowerFOV(float s)
    {
        float t = 0f;
        float minimum = s;
        float maximum = standardFOV;

        t += 2f * Time.deltaTime;
        currentFOV = Mathf.Lerp(minimum, maximum, t);

        playerCamera.fieldOfView = currentFOV;
    }


}
