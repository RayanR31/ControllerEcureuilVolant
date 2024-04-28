using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataController
{
    public enum States
    {
        move,
    }

    [Serializable]
    public struct StructController
    {
        public GameObject Controller_go;
        public States CurrentStates;
        public States TargetStates;
        public Vector3 destination;
        public Vector3 direction;
        public Vector3 normal;
        public float currentSpeed;
        public float currentVelocity;
        public bool ChangeState;
    }

    [Serializable]
    public struct StructCamera
    {
        public GameObject camera_go;
        public Vector3 direction_cam;
    }
}
