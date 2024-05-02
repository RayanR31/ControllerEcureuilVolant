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
    public static Vector3 CalculNormal(ref StructController _dataController, StructCamera _dataCamera, float radius = 1f,int resolution = 30)
    {
        if (InputManager.GetInputMove() != Vector2.zero)
        {
            _dataController.Controller_go.transform.rotation = Quaternion.Euler(0, _dataCamera.direction_cam.y, 0);
        }

        _dataController.Controller_go.transform.rotation = Quaternion.FromToRotation(_dataController.Controller_go.transform.up, _dataController.averageNormal) * _dataController.Controller_go.transform.rotation;

        int hitCount = 0; // Compte le nombre de hits
        Vector3 pos = new Vector3(_dataController.Controller_go.transform.position.x, _dataController.Controller_go.transform.position.y, _dataController.Controller_go.transform.position.z);
        for (int j = 0; j <= 36; j++)
        {
            float angleStep = Mathf.PI / resolution;

            Vector3 previousPoint = _dataController.Controller_go.transform.rotation * Quaternion.Euler(0, j * 10, 0) * pos;

            for (int i = 0; i <= resolution; i++)
            {
                float angle = i * angleStep;

                Vector3 nextPoint = pos + (_dataController.Controller_go.transform.rotation * Quaternion.Euler(0, j * 10, 0) * new Vector3(Mathf.Sin(angle),Mathf.Cos(angle), 0 * Mathf.Sin(angle)) * radius);

                if (i > 0)
                {
                    //Debug.DrawLine(previousPoint, nextPoint, Color.blue);

                    if (Physics.Raycast(previousPoint, nextPoint - previousPoint, out RaycastHit hit, Vector3.Distance(nextPoint, previousPoint)))
                    {
                        _dataController.averageNormal += hit.normal; 
                        hitCount++;
                        break;
                    }
                }

                previousPoint = nextPoint;
            }
        }

        if (hitCount > 0)
        {
            _dataController.averageNormal /= hitCount; // Divise la somme par le nombre total de hits pour obtenir la moyenne
        }

        //Debug.Log("Moyenne des normales touchées : " + hitY);

        return _dataController.averageNormal;

    }
    public static Vector3 SearchPosition(ref StructController _dataController, StructCamera _dataCamera , float _x , float _y , float _z ,float radius = 1f , int resolution = 30)
    {
        if (InputManager.GetInputMove() != Vector2.zero)
        {
            _dataController.Controller_go.transform.rotation = Quaternion.Euler(0, _dataCamera.direction_cam.y, 0);
        }

        _dataController.Controller_go.transform.rotation = Quaternion.FromToRotation(_dataController.Controller_go.transform.up, _dataController.averageNormal) * _dataController.Controller_go.transform.rotation;

        float angleStep = Mathf.PI / resolution;
        Vector3 previousPoint = _dataController.Controller_go.transform.rotation * _dataController.Controller_go.transform.position;

        for (int i = 0; i <= resolution; i++)
        {
            float angle = i * angleStep;

            Vector3 nextPoint = _dataController.Controller_go.transform.position + (_dataController.Controller_go.transform.rotation * new Vector3(_x * Mathf.Sin(angle), _y * Mathf.Cos(angle), _z * Mathf.Sin(angle)) * radius);

            if (i > 0)
            {
                Debug.DrawRay(previousPoint, (nextPoint - previousPoint) * 1, Color.red); 
                if (Physics.Raycast(previousPoint, nextPoint - previousPoint, out RaycastHit hit, Vector3.Distance(nextPoint, previousPoint)))
                {
                    //_dataController.averageNormal = hit.normal;
                    return hit.point;
                }
            }

            previousPoint = nextPoint;
        }

        return _dataController.destination;

    }
    public static bool CheckWall(ref StructController _dataController, float radius = 1f, int resolution = 30)
    {
        int hitCount = 0; // Compte le nombre de hits
        Vector3 pos = new Vector3(_dataController.Controller_go.transform.position.x, _dataController.Controller_go.transform.position.y, _dataController.Controller_go.transform.position.z);
        for (int j = 0; j <= 36; j++)
        {
            float angleStep = Mathf.PI / resolution;

            Vector3 previousPoint = _dataController.Controller_go.transform.rotation * Quaternion.Euler(0, j * 10, 0) * pos;

            for (int i = 0; i <= resolution; i++)
            {
                float angle = i * angleStep;

                Vector3 nextPoint = pos + (_dataController.Controller_go.transform.rotation * Quaternion.Euler(0, j * 10, 0) * new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0 * Mathf.Sin(angle)) * radius);

                if (i > 0)
                {
                    Debug.DrawLine(previousPoint, nextPoint, Color.blue);

                    if (Physics.Raycast(previousPoint, nextPoint - previousPoint, out RaycastHit hit, Vector3.Distance(nextPoint, previousPoint)))
                    {
                        _dataController.averageNormal += hit.normal;
                        hitCount++;
                        return true;
                    }
                }

                previousPoint = nextPoint;
            }
        }

        if (hitCount > 0)
        {
            _dataController.averageNormal /= hitCount; // Divise la somme par le nombre total de hits pour obtenir la moyenne
        }

        return false;
    }
}
