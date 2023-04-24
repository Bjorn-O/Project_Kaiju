using System;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairMovement : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Image crosshair;
    

    public void UpdateCrosshair(Vector2 aimPoint)
    {
        //print(aimPoint);
        crosshair.rectTransform.position = camera.WorldToScreenPoint(aimPoint); 
    }
}
