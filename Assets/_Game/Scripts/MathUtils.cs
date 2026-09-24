using UnityEngine;

static public class MathUtils
{
    static public Vector3 LinearBezier(Vector3 A, Vector3 B, float t)
    {
        return (1-t)*A+t*B;
    }

    static public Vector3 QuadraticBezier(Vector3 A, Vector3 B, Vector3 C, float t)
    {
        return (1-t)*LinearBezier(A,B,t)+t*LinearBezier(B,C,t);
    }

    static public Vector3 CubicBezier(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t)
    {
        return (1-t)*QuadraticBezier(A,B,C,t)+t*QuadraticBezier(B,C,D,t);
    }
    
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
