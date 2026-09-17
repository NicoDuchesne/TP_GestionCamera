using UnityEngine;

public class FixedView : AView
{
    public float yaw;
    public float pitch;
    public float roll;
    public float fov;

    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration result = new CameraConfiguration();
        result.yaw = yaw;
        result.pitch = pitch;
        result.roll = roll;
        result.fov = fov;
        
        result.pivot = this.transform.position;
        result.distance = Vector3.zero;
        return result;
    }
}
