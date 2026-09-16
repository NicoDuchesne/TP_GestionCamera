using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    private CameraConfiguration configuration;
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
    
    private void Update()
    {
        ApplyConfiguration();
        configuration = ComputeAverage();
    }
    
    void OnDrawGizmos()
    {
        configuration.DrawGizmos(Color.red);
    }
    
    private void ApplyConfiguration()
    {
        
    }

    public void AddView(AView view)
    {
        activeViews.Add(view);
    }

    public void RemoveView(AView view)
    {
        activeViews.Remove(view);
    }

    public CameraConfiguration ComputeAverage()
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
    
}
