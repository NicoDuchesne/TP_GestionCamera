using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreeFollowView : AView
{
    public float[] pitch = new float[3];
    public float[] roll = new float[3];
    public float[] fov = new float[3];
    public float yaw;
    public float yawSpeed;
    public GameObject target;
    public Curve curve;
    public float curvePosition = 0.5f;
    public float curveSpeed;
    [SerializeField] private InputActionReference inputMove;

    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration result = new CameraConfiguration();
        Vector3 a = new Vector3(pitch[0],roll[0],fov[0]);
        Vector3 b = new Vector3(pitch[1],roll[1],fov[1]);
        Vector3 c = new Vector3(pitch[2],roll[2],fov[2]);
        Vector3 cam = MathUtils.QuadraticBezier(a,b,c,curvePosition);
        result.pivot = curve.GetPosition(curvePosition, curve.transform.localToWorldMatrix);
        result.pitch = cam.x;
        result.roll = cam.y;
        result.fov = cam.z;
        result.yaw = yaw - 90f;
        return result;
    }

    private void Update()
    {
        if(inputMove.action.ReadValue<Vector2>().y>0)
        {
            curvePosition += curveSpeed*Time.deltaTime;
        }
        else if (inputMove.action.ReadValue<Vector2>().y<0)
        {
            curvePosition -= curveSpeed*Time.deltaTime;
        }
        if(inputMove.action.ReadValue<Vector2>().x>0)
        {
            yaw += yawSpeed*Time.deltaTime;
        }
        else if (inputMove.action.ReadValue<Vector2>().x<0)
        {
            yaw -= yawSpeed*Time.deltaTime;
        }
        Matrix4x4 curveToWorldMatrix = Matrix4x4.TRS(target.transform.position, Quaternion.Euler(0f, yaw, 0f), Vector3.one);
        curve.transform.position = curveToWorldMatrix.GetPosition();
        curve.transform.rotation = curveToWorldMatrix.rotation;
        curvePosition = Mathf.Clamp01(curvePosition);
    }

    private void OnDrawGizmos()
    {
        curve.DrawGizmo(Color.red,transform.localToWorldMatrix);
    }
}
