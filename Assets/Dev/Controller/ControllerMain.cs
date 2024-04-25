using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataController;

public class ControllerMain : MonoBehaviour
{

    [SerializeField] public StructController dataController = new StructController(); 
    [SerializeField] public StructCamera dataCamera = new StructCamera(); 

    [SerializeField] public GameObject cam ; 
    [SerializeField] public GameObject pointYCamera ;


    public List<IPlayerState> IPlayerStateArray = new List<IPlayerState>();

    public List<DataScriptableObject> DataScriptableObjectList = new List<DataScriptableObject>();

    // Start is called before the first frame update
    void Awake()
    {
        dataController.normal = Vector3.up; 
        dataController.destination = transform.position;
        InitState(); 
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        PlayStateCurrent();

    }
    private void Update()
    {

        TransitionStates(); 
        dataController.Controller_go.transform.position = dataController.destination;
        
        Camera_roue_de_secours();
    }
    void InitState()
    {
        IPlayerStateArray.Add(new CS_Move_R());
    }
    void PlayStateCurrent()
    {
        IPlayerStateArray[(int)dataController.CurrentStates].CurrentStateUpdate(ref dataController, dataCamera , DataScriptableObjectList[(int)dataController.CurrentStates]);
        IPlayerStateArray[(int)dataController.CurrentStates].ChangeStateByInput(ref dataController);
        IPlayerStateArray[(int)dataController.CurrentStates].ChangeStateByNature(ref dataController, dataCamera);
    }
    void TransitionStates()
    {
        if (dataController.ChangeState)
        {
            IPlayerStateArray[(int)dataController.CurrentStates].ExitState(dataController);
            dataController.CurrentStates = dataController.TargetStates;
            IPlayerStateArray[(int)dataController.CurrentStates].EnterState(dataController);
            dataController.ChangeState = false;
        }
    }





    public float timer;
    Vector3 pos; 
    void Camera_roue_de_secours()
    {
        dataCamera.direction_cam = cam.transform.eulerAngles;
        transform.eulerAngles = new Vector3(0, dataCamera.direction_cam.y, 0 );
        if(dataController.CurrentStates == States.move || timer > 0.3f)
        {
            pointYCamera.transform.position = Vector3.Lerp(pointYCamera.transform.position, dataController.destination,Time.deltaTime * 3f);
            pos = pointYCamera.transform.position;
            if(dataController.CurrentStates == States.move) timer = 0;

        }
        else
        {
            timer += Time.deltaTime;
            pos = new Vector3(dataController.destination.x, pointYCamera.transform.position.y, dataController.destination.z);
            pointYCamera.transform.position = Vector3.Lerp(pointYCamera.transform.position, pos, Time.deltaTime * 3f);
        }
    }
    Vector3 velocity;

    void OnDrawGizmos()
    {
        velocity = Vector3.Lerp(velocity,new Vector3(InputManager.inputMove.x, 1, InputManager.inputMove.y), Time.deltaTime * 6f); 
        PhysicsCustom.DrawCircle(ref dataController.Controller_go, velocity.x , 1 , velocity.z, Color.red);
        //PhysicsCustom.DrawCircle(ref dataController.Controller_go, 0, 1 , 1, Color.red);
        //PhysicsCustom.DrawCircle(ref dataController.Controller_go, 0, 1, 1, Color.red);
    }

}
