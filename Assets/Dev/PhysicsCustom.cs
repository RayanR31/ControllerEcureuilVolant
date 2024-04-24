using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.HableCurve;

public class PhysicsCustom : MonoBehaviour
{
    public int Resolution = 10;
    public float radius = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDrawGizmos()
    {
       /* DrawCircle(1,1,0,Color.red);
        DrawCircle(-1,1,0,Color.yellow);
        DrawCircle(0,1,1,Color.blue);
        DrawCircle(0,1,-1,Color.magenta);*/


        SearchPosition(1, 1, 0, Color.red);

    }

    void DrawCircle(float _x , float _y , float _z , Color _color)
    {
        float angleStep = Mathf.PI / Resolution;
        Vector3 previousPoint = transform.position;

        for (int i = 0; i <= Resolution; i++)
        {
            float angle = i * angleStep;

            Vector3 nextPoint = transform.position + new Vector3(_x * Mathf.Sin(angle), _y * Mathf.Cos(angle), _z * Mathf.Sin(angle)) * radius;

            if (i > 0)
            {
                Gizmos.color = _color;   
                Gizmos.DrawLine(previousPoint, nextPoint);
            }

            previousPoint = nextPoint;
        }
    }

    void SearchPosition(float _x, float _y, float _z, Color _color)
    {
        float angleStep = Mathf.PI / Resolution;
        Vector3 previousPoint = transform.position;

        for (int i = 0; i <= Resolution; i++)
        {
            float angle = i * angleStep;

            Vector3 nextPoint = transform.position + new Vector3(_x * Mathf.Sin(angle), _y * Mathf.Cos(angle), _z * Mathf.Sin(angle)) * radius;

            if (i > 0)
            {
                Gizmos.color = _color;
                Gizmos.DrawLine(previousPoint, nextPoint);
                if(Physics.Raycast(previousPoint, nextPoint - previousPoint, out RaycastHit hit , Vector3.Distance(nextPoint , previousPoint)))
                {
                    Debug.Log(hit.point);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(previousPoint, nextPoint);
                    return; 
                }
            }

            previousPoint = nextPoint;
        }
    }
}
