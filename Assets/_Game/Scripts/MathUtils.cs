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
}
