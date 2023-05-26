using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmarker : MonoBehaviour
{
    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameObject.SetActive(false);
    }

    public void Hit()
    {
        HitOn();
        audioManager.Play("Hit");
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
