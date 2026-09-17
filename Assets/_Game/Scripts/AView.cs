using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float weight;
    public bool isActiveOnStart;

    private void Start()
    {
        if (isActiveOnStart) SetActive(isActiveOnStart);
    }
    public virtual CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration();
    }

    public void SetActive(bool isActive)
    {
        CameraController.Instance.AddView(this);
    }
}
