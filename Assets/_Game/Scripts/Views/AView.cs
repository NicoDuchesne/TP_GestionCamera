using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float weight;
    
    public virtual CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration();
    }

    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            CameraController.Instance.AddView(this);
        }
        else
        {
            weight = 0f;
            CameraController.Instance.RemoveView(this);
        }
    }

    void OnDrawGizmos()
    {
        GetConfiguration().DrawGizmos(Color.blue);
    }
}
