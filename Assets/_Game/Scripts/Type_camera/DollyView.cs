using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DollyView : AView
{
    [SerializeField] private float roll;
    [SerializeField] private Vector3 distance;
    [SerializeField] private float fov;
    
    [SerializeField] private GameObject target;
    [SerializeField] private Rail rail;
    [SerializeField] private float speed;
    [SerializeField] private InputActionReference moveAction;

    public bool isAuto;
    private float distanceOnRail = 0f;

    private void Update()
    {
        if (isAuto)
        {
            distanceOnRail = rail.GetDistanceOnRailOfNearestPoint(target.transform.position);
        }
        else
        {
            float horizontal = moveAction.action.ReadValue<Vector2>().x;
            MoveRail(horizontal);
        }
        
    }

    private void MoveRail(float input)
    {
        if (!rail.isLoop)
        {
            distanceOnRail = Mathf.Clamp(distanceOnRail + input * speed * Time.deltaTime, 0f, rail.GetLength()-1);
        }
        else
        {
            distanceOnRail += input * speed * Time.deltaTime;
        }
    }
    
    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration result = new CameraConfiguration();
        
        Vector3 dir = (target.transform.position - Camera.main.transform.position).normalized;
        float yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float pitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        
        result.roll = roll;
        result.distance = distance;
        result.fov = fov;
        result.yaw = yaw;
        result.pitch = pitch;
        result.pivot = rail.GetPosition(distanceOnRail);
        
        return result;
    }
}
