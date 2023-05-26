using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private Sound start;
    private Sound loop;

    private void Awake()
    {
        foreach (var s in sounds)
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

        var s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        { 
            Debug.LogWarning("sound " + name + " not found");
            return;
        }

        switch (name)
        {
            case "BoatAccelStart":
                StartCoroutine(AccelStart());
                return;
            case "BoatAccelStop":
                start.audioSource.Stop();
                loop.audioSource.Stop();
                StopAllCoroutines();
                s.audioSource.Play();
                return;
            default:
                s.audioSource.Play();
                break;
        }
    }

    private IEnumerator AccelStart()
    {
        start.audioSource.Play();
        yield return new WaitForSeconds(2.3f);       
        loop.audioSource.Play();
    }



}