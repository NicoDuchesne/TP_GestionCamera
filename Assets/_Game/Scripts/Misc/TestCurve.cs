using UnityEngine;

public class TestCurve : MonoBehaviour
{
    public Curve curve;

    private void OnDrawGizmos()
    {
        curve.DrawGizmo(Color.red,transform.localToWorldMatrix);
    }
}
