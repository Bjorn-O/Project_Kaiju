using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    Sound start;
    Sound loop;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }

        start = Array.Find(sounds, sound => sound.name == "BoatAccelStart");
        loop = Array.Find(sounds, sound => sound.name == "BoatAccel");
    }


    public void Play(string name)// to play the music you need to referance this function in the script where you want to play it with as paramater the name of the sound
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("sound " + name + " not found");
            return;
        }

        if (name == "BoatAccelStart")
        {
            StartCoroutine(AccelStart());
            return;
        }

        if(name == "BoatAccelStop")
        {
            start.audioSource.Stop();
            loop.audioSource.Stop();
            StopAllCoroutines();
            s.audioSource.Play();
            return;
        }

        s.audioSource.Play();
    }
        
    IEnumerator AccelStart()
    {
        start.audioSource.Play();
        yield return new WaitForSeconds(2.3f);       
        loop.audioSource.Play();
    }



}