using Unity.VisualScripting;
using UnityEngine;

public static class MathUtils
{
    public static Vector3 GetNearestPointOnSegment(Vector3 a, Vector3 b, Vector3 target)
    {
        Vector3 ac = (target - a);
        Vector3 n = (b-a).normalized;
        float dot = Vector3.Dot(ac, n);
        dot = Mathf.Clamp(dot, 0, Vector3.Distance(a, b));
        Vector3 proj = a + n * dot;

        return proj;
    }
}
