using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[SaveDuringPlay]
[AddComponentMenu("")]
public class CinCamClampExt : CinemachineExtension
{
    [Tooltip("Camera will clamp relative to this transform")]
    public Transform refOrientation;
    [Tooltip("Max X/Y angle the camera can turn")]
    public Vector2 angleBounds;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Aim)
        {
            Quaternion yCamOnly = Quaternion.Euler(0, state.RawOrientation.eulerAngles.y, 0);
            Quaternion yRefOnly = Quaternion.Euler(0, refOrientation.rotation.eulerAngles.y, 0);
            float yAngle = Quaternion.Angle(yCamOnly, yRefOnly);
            if (yAngle > angleBounds.y)
            {
                yCamOnly = Quaternion.Euler(0, state.RawOrientation.eulerAngles.y + 1, 0);
                if (yAngle < Quaternion.Angle(yCamOnly, yRefOnly))
                {
                    state.RawOrientation = Quaternion.Euler(state.RawOrientation.eulerAngles.x, state.RawOrientation.eulerAngles.y - (yAngle - angleBounds.y), state.RawOrientation.eulerAngles.z);
                }
                else
                {
                    state.RawOrientation = Quaternion.Euler(state.RawOrientation.eulerAngles.x, state.RawOrientation.eulerAngles.y + (yAngle - angleBounds.y), state.RawOrientation.eulerAngles.z);
                }
            }

            Quaternion xCamOnly = Quaternion.Euler(state.RawOrientation.eulerAngles.x, 0, 0);
            Quaternion xRefOnly = Quaternion.Euler(refOrientation.rotation.eulerAngles.x, 0, 0);
            float xAngle = Quaternion.Angle(xCamOnly, xRefOnly);
            if (xAngle > angleBounds.x)
            {
                xCamOnly = Quaternion.Euler(state.RawOrientation.eulerAngles.x + 1, 0, 0);
                if (xAngle < Quaternion.Angle(xCamOnly, xRefOnly))
                {
                    state.RawOrientation = Quaternion.Euler(state.RawOrientation.eulerAngles.x - (xAngle - angleBounds.x), state.RawOrientation.eulerAngles.y, state.RawOrientation.eulerAngles.z);
                }
                else
                {
                    state.RawOrientation = Quaternion.Euler(state.RawOrientation.eulerAngles.x + (xAngle - angleBounds.x), state.RawOrientation.eulerAngles.y, state.RawOrientation.eulerAngles.z);
                }
            }
        }
    }
}