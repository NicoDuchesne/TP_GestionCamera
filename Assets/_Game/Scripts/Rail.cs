using UnityEngine;
using UnityEngine.UIElements;

public class Rail : MonoBehaviour
{
    public bool isLoop;
    private float length;

    private void Start()
    {
        CalculateLength();
    }
    void OnDrawGizmos()
    {
        DrawGizmos(Color.black);
    }

    public float GetLength()
    {
        return length;
    }

    private void CalculateLength()
    {
        float result = 0f;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 pos = transform.GetChild(i).position;

            if (i + 1 < transform.childCount)
            {
                Vector3 nextPos = transform.GetChild(i+1).position;
                result += Vector3.Distance(pos, nextPos);
            }

            if (i == transform.childCount - 1 && isLoop && transform.childCount > 0)
            {
                Vector3 firstPos = transform.GetChild(0).position;
                result += Vector3.Distance(pos, firstPos);
            }
        }
        
        length = result;
    }

    public Vector3 GetPosition(float distance)
    {
        distance = Mathf.Repeat(distance, length);
        float traveled = 0f;
        Vector3 result = Vector3.zero;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 pos = transform.GetChild(i).position;
            Vector3 nextPos = pos;
            
            if (i + 1 < transform.childCount)
            {
                nextPos = transform.GetChild(i+1).position;
            }

            if (i == transform.childCount - 1 && isLoop && transform.childCount > 0)
            {
                nextPos = transform.GetChild(0).position;
            }
            
            float d = distance - traveled;
            traveled += Vector3.Distance(pos, nextPos);
            
            if (traveled > distance)
            {
                Vector3 dir = (nextPos-pos).normalized;
                result = pos + dir * d;
                return result;
            }
        }
        
        return result;
    }
    
    private void DrawGizmos(Color color)
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
