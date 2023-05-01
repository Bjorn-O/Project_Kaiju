using UnityEngine;

public class TestAudio : MonoBehaviour
{

    [SerializeField] private AudioManager audioManager;   

    public void AudioTestFunction()
    {
        audioManager.Play("GunShoot");
    }

}
