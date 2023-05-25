using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmarker : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Hit()
    {
        HitOn();
        //Play Sound
        Invoke("HitOff", 0.2f);
    }
    
    private void HitOn()
    {
        gameObject.SetActive(true);
    }

    private void HitOff()
    {
        gameObject.SetActive(false);
    }


}
