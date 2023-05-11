using UnityEngine;
using UnityEngine.InputSystem;

public class Recoil : MonoBehaviour
{
    [SerializeField] private CrosshairMovement crosshairMovement;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float recoilAmount = 10f;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float recoilX = 10f;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float recoilY = 10f;

    public void RecoilOnFire()
    {
            // Move the desired location vector upwards by the recoil amount
            crosshairMovement.DesiredLocation += new Vector3(Random.Range(-recoilX, recoilX), Random.Range(0f,recoilY), 0) * recoilAmount;    
    }

}