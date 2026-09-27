using System;
using UnityEngine;

public class FixedFollowView : AView
{
    public float roll;
    public float fov;
    public GameObject target;
    public GameObject centralPoint;
    public float yawOffsetMax;
    public float pitchOffsetMax;

    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration result = new CameraConfiguration();
        Vector3 dir = (target.transform.position - Camera.main.transform.position).normalized;
        float yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        if (yawOffsetMax > yaw && -yawOffsetMax < yaw)
        {
            result.yaw = yaw;
        }
        else if (yawOffsetMax < yaw)
        {
            result.yaw = yawOffsetMax;
        }
        else
        {
            result.yaw = -yawOffsetMax;
        }
        float pitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        if (pitchOffsetMax > pitch && -pitchOffsetMax < pitch)
        {
            result.pitch = pitch;
        }
        else if (pitchOffsetMax < pitch)
        {
            result.pitch = pitchOffsetMax;
        }
        else
        {
            result.pitch = -pitchOffsetMax;
        }
        result.roll = roll;
        result.fov = fov;
        result.pivot = this.transform.position;
        return result;
    }
}
