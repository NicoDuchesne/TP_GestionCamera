using UnityEngine;

public abstract class AViewVolume : MonoBehaviour
{
    public int priority = 0;
    public AView view;

    private int uid;

    public int Uid => uid;
    
    private static int nextUID = 0;

    protected bool isActive;

    public bool IsActive
    {
        get { return isActive; }
        private set { isActive = value; }
    }

    public virtual void Awake()
    {
        uid = nextUID;
        nextUID++;
    }
    
    public virtual float ComputeSelfWeight()
    {
        return 1f;
    }

    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            ViewVolumeBlender.Instance.AddVolume(this);
        }
        else
        {
            ViewVolumeBlender.Instance.RemoveVolume(this);
        }
        this.isActive = isActive;
    }
}
