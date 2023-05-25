using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    AudioManager audioManager;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Accel()
    {
        audioManager.Play("BoatAccelStart");
    }

    public void Decel() 
    {
        audioManager.Play("BoatAccelStop");
    }

}
