using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public float smoothSpeed = 5f;
    private CameraConfiguration targetConfiguration;
    private CameraConfiguration actualConfiguration;
    private bool isCutRequested;
    public static CameraController Instance;
    
    private List<AView> activeViews = new List<AView>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        actualConfiguration = ComputeAverage();
        ApplyConfiguration(cam, actualConfiguration);
        targetConfiguration = actualConfiguration;
    }
    
    private void Update()
    {
        targetConfiguration = ComputeAverage();
        actualConfiguration = Smooth();
        ApplyConfiguration(cam, actualConfiguration);
    }
    
    void OnDrawGizmos()
    {
        targetConfiguration.DrawGizmos(Color.red);
        actualConfiguration.DrawGizmos(Color.green);
    }
    
    private void ApplyConfiguration(Camera c, CameraConfiguration config)
    {
        Transform cameraTransform = c.transform;
        cameraTransform.position = config.GetPosition();
        cameraTransform.rotation = config.GetRotation();
        c.fieldOfView = config.fov;
    }

    private CameraConfiguration Smooth()
    {
        if (isCutRequested)
        {
            isCutRequested = false;
            return targetConfiguration;
        }
        
        CameraConfiguration result = actualConfiguration;
        
        if (smoothSpeed * Time.deltaTime < 1)
        {
            result.pitch += (targetConfiguration.pitch - actualConfiguration.pitch) *  smoothSpeed * Time.deltaTime;
            result.roll += (targetConfiguration.roll - actualConfiguration.roll) *  smoothSpeed * Time.deltaTime;
            
            result.pivot += (targetConfiguration.pivot - actualConfiguration.pivot) *  smoothSpeed * Time.deltaTime;
            result.distance += (targetConfiguration.distance - actualConfiguration.distance) *  smoothSpeed * Time.deltaTime;
            result.fov += (targetConfiguration.fov - actualConfiguration.fov) *  smoothSpeed * Time.deltaTime;
            
            Vector2 yawActual = new Vector2(Mathf.Cos(actualConfiguration.yaw * Mathf.Deg2Rad), 
                Mathf.Sin(actualConfiguration.yaw * Mathf.Deg2Rad));
            Vector2 yawTarget = new Vector2(Mathf.Cos(targetConfiguration.yaw * Mathf.Deg2Rad), 
                Mathf.Sin(targetConfiguration.yaw * Mathf.Deg2Rad));
            Vector2 yaw = yawActual + (yawTarget - yawActual) *  smoothSpeed * Time.deltaTime;
            result.yaw = Vector2.SignedAngle(Vector2.right, yaw);
            
        }
        else
        {
            result = targetConfiguration;
        }

        return result;
    }

    public void AddView(AView view)
    {
        activeViews.Add(view);
    }

    public void RemoveView(AView view)
    {
        activeViews.Remove(view);
    }

    private CameraConfiguration ComputeAverage()
    {
        CameraConfiguration result = new CameraConfiguration();
        float totalWeight = 0;
        
        foreach (AView view in activeViews)
        {
            CameraConfiguration c = view.GetConfiguration();
            totalWeight += view.weight;
            
            result.pitch += c.pitch * view.weight;
            result.roll += c.roll * view.weight;
            result.pivot += c.pivot * view.weight;
            result.distance += c.distance * view.weight;
            result.fov += c.fov * view.weight;
        }

        if (totalWeight == 0)
        {
            return new CameraConfiguration();
        }
        
        result.pitch /= totalWeight;
        result.roll /= totalWeight;
        result.pivot /= totalWeight;
        result.distance /= totalWeight;
        result.fov /= totalWeight;

        result.yaw = ComputeAverageYaw();

        return result;
    }
    
    public float ComputeAverageYaw()
    {
        Vector2 sum = Vector2.zero;
        foreach (AView view in activeViews)
        {
            CameraConfiguration config = view.GetConfiguration();
            sum += new Vector2(Mathf.Cos(config.yaw * Mathf.Deg2Rad),
                Mathf.Sin(config.yaw * Mathf.Deg2Rad)) * view.weight;
        }
        return Vector2.SignedAngle(Vector2.right, sum);
    }

    public void Cut()
    {
        isCutRequested = true;
    }
    
}
