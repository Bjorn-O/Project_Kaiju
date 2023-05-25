using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume = 1;
    [Range(0f, 1f)]
    public float pitch = 1;

    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;

}
