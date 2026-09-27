using System;
using UnityEngine;

public class Curve : MonoBehaviour
{
    public Vector3 A;
    public Vector3 B;
    public Vector3 C;
    public Vector3 D;

    public Vector3 GetPosition(float t)
    {
        return MathUtils.CubicBezier(A,B,C,D,t);
    }

    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
    {
        return localToWorldMatrix.MultiplyPoint(GetPosition(t));
    }
    
    public void DrawGizmo(Color c, Matrix4x4 localToWorldMatrix)
    {
        Gizmos.color = c;
        Gizmos.matrix = localToWorldMatrix;
        Gizmos.DrawSphere(A, 0.1f);
        Gizmos.DrawSphere(B, 0.1f);
        Gizmos.DrawSphere(C, 0.1f);
        Gizmos.DrawSphere(D, 0.1f);
        for (int i=0;i<100;i++)
        {
            Gizmos.DrawLine(GetPosition(i / 100f, localToWorldMatrix), GetPosition((i + 1) / 100f, localToWorldMatrix));
        }
    }
}
