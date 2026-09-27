using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggeredViewVolume : AViewVolume
{
    private Collider col;
    public TagHandle target;
    
    public override void Awake()
    {
        base.Awake();
        col = GetComponent<Collider>();
        col.isTrigger = true;

        target = TagHandle.GetExistingTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter : " + other.gameObject.tag);
        if (other.gameObject.CompareTag(target))
        {
            SetActive(true);
            Debug.Log("OnTriggerEnter");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(target))
        {
            SetActive(false);
            Debug.Log("OnTriggerExit");
        }
    }
    
}
