using UnityEngine;

public class Rail : MonoBehaviour
{
    public bool isLoop;
    
    void OnDrawGizmos()
    {
        DrawGizmos(Color.black);
    }
    
    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Gizmos.DrawSphere(child.position, 0.25f);

            if (i + 1 < transform.childCount)
            {
                Transform nextChild = transform.GetChild(i+1);
                Gizmos.DrawLine(child.position, nextChild.position);
            }

            if (i == transform.childCount - 1 && isLoop && transform.childCount > 0)
            {
                Transform firstChild = transform.GetChild(0);
                Gizmos.DrawLine(child.position, firstChild.position);
            }
        }
        
        
    }
}
