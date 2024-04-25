using UnityEngine;

public class PhysicsCustom
{

    public static void DrawCircle(ref GameObject gameObject , float _x , float _y , float _z , Color _color, int resolution = 30, float radius = 1.5f)
    {
        float angleStep = Mathf.PI / resolution;
        Vector3 previousPoint =  gameObject.transform.position;

        for (int i = 0; i <= resolution; i++)
        {
            float angle = i * angleStep;

            Vector3 nextPoint = gameObject.transform.position + new Vector3(_x * Mathf.Sin(angle), _y * Mathf.Cos(angle), _z * Mathf.Sin(angle)) * radius;

            if (i > 0)
            {
                Gizmos.color = _color;   
                Gizmos.DrawLine(previousPoint, nextPoint);
            }

            previousPoint = nextPoint;
        }
    }

    public static Vector3 SearchPosition(ref GameObject gameObject , float _x, float _y, float _z, Color _color , int resolution = 30 , float radius = 1.5f)
    {
        float angleStep = Mathf.PI / resolution;
        Vector3 previousPoint = gameObject.transform.position;

        for (int i = 0; i <= resolution; i++)
        {
            float angle = i * angleStep;

            Vector3 nextPoint = gameObject.transform.position + new Vector3(_x * Mathf.Sin(angle), _y * Mathf.Cos(angle), _z * Mathf.Sin(angle)) * radius;

            if (i > 0)
            {
                if(Physics.Raycast(previousPoint, nextPoint - previousPoint, out RaycastHit hit , Vector3.Distance(nextPoint , previousPoint)))
                {
                    return hit.point; 
                }
            }

            previousPoint = nextPoint;
        }

        return gameObject.transform.position;

    }
}
