using System;
using UnityEngine;

public class SphereViewVolume : AViewVolume
{
    public GameObject target;
    public float outerRadius;
    public float innerRadius;
    private float distance;

    void Update()
    {
        distance = Vector3.Distance(transform.position,target.transform.position);
        if(distance<=outerRadius && !IsActive)
        {
            SetActive(true);
        }
        if(distance>outerRadius && IsActive)
        {
            SetActive(false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, innerRadius);
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }

    public override float ComputeSelfWeight()
    {
        float result = Mathf.Clamp01(1-(distance-innerRadius)/(outerRadius-innerRadius));
        return result;
    }
}
