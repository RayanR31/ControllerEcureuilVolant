using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataController", menuName = "Data/ControllerData")]
public class DataScriptableObject : ScriptableObject
{
    public float speed;
    public float angularDrag;
    public float pourcentageMagnitude = 1;
    public float gravity = 1;
    public AnimationCurve curve;
}
