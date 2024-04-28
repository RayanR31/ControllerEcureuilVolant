using UnityEngine;
using static DataController;

public class PhysicsCustom
{

    public static void DrawCircle(ref GameObject gameObject, StructCamera _dataCamera, float _x, float _y, float _z, Color _color, int resolution = 30, float radius = 1f)
    {
        float angleStep = Mathf.PI / resolution;
        Vector3 previousPoint =  gameObject.transform.rotation * gameObject.transform.position;
        for (int i = 0; i <= resolution; i++)
        {
            float angle = i * angleStep;

            Vector3 nextPoint =  gameObject.transform.position + (gameObject.transform.rotation * new Vector3(_x * Mathf.Sin(angle), _y * Mathf.Cos(angle), _z * Mathf.Sin(angle)) * radius);
            if (i > 0)
            {
                Gizmos.color = _color;
                Gizmos.DrawLine(previousPoint, nextPoint);
            }

            previousPoint = nextPoint;
        }
    }

    public static Vector3 averageNormal;
    public static Vector3 CalculNormal(GameObject gameObject, StructCamera _dataCamera, int resolution = 30, float radius = 1f)
    {
        if (InputManager.GetInputMove() != Vector2.zero)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, _dataCamera.direction_cam.y, 0);
        }

        gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.up, averageNormal) * gameObject.transform.rotation;

        int hitCount = 0; // Compte le nombre de hits

        for (int j = 0; j <= 36; j++)
        {
            float angleStep = Mathf.PI / resolution;

            Vector3 previousPoint = gameObject.transform.rotation * Quaternion.Euler(0, j * 10, 0) * gameObject.transform.position;

            for (int i = 0; i <= resolution; i++)
            {
                float angle = i * angleStep;

                Vector3 nextPoint = gameObject.transform.position + (gameObject.transform.rotation * Quaternion.Euler(0, j * 10, 0) * new Vector3(Mathf.Sin(angle),Mathf.Cos(angle), 0 * Mathf.Sin(angle)) * radius);

                if (i > 0)
                {
                    Debug.DrawLine(previousPoint, nextPoint, Color.blue);

                    if (Physics.Raycast(previousPoint, nextPoint - previousPoint, out RaycastHit hit, Vector3.Distance(nextPoint, previousPoint)))
                    {
                        averageNormal += hit.normal; 
                        hitCount++;
                        break;
                    }
                }

                previousPoint = nextPoint;
            }
        }

        if (hitCount > 0)
        {
             averageNormal /= hitCount; // Divise la somme par le nombre total de hits pour obtenir la moyenne
        }

        Debug.Log("Moyenne des normales touchées : " + averageNormal);

        return averageNormal;

    }

    public static Vector3 SearchPosition(ref GameObject gameObject, StructCamera _dataCamera, float _x, float _y, float _z, Color _color, int resolution = 30, float radius = 1f)
    {
        if (InputManager.GetInputMove() != Vector2.zero)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, _dataCamera.direction_cam.y, 0);
        }

        gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.up, averageNormal) * gameObject.transform.rotation;

        float angleStep = Mathf.PI / resolution;
        Vector3 previousPoint = gameObject.transform.rotation * gameObject.transform.position;

        for (int i = 0; i <= resolution; i++)
        {
            float angle = i * angleStep;

            Vector3 nextPoint = gameObject.transform.position + (gameObject.transform.rotation * new Vector3(_x * Mathf.Sin(angle), _y * Mathf.Cos(angle), _z * Mathf.Sin(angle)) * radius);

            if (i > 0)
            {
                //Debug.DrawRay(previousPoint, (nextPoint - previousPoint) * Vector3.Distance(nextPoint, previousPoint)); 
                if (Physics.Raycast(previousPoint, nextPoint - previousPoint, out RaycastHit hit, Vector3.Distance(nextPoint, previousPoint)))
                {
                    return hit.point;
                }
            }

            previousPoint = nextPoint;
        }

        return gameObject.transform.position;

    }
}
