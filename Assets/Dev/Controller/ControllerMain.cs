using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataController;

public class ControllerMain : MonoBehaviour
{

    [SerializeField] public StructController dataController = new StructController(); 
    [SerializeField] public StructCamera dataCamera = new StructCamera(); 




    public List<IPlayerState> IPlayerStateArray = new List<IPlayerState>();

    public List<DataScriptableObject> DataScriptableObjectList = new List<DataScriptableObject>();

    [Header("TEMPORARY - Camera - Character")]
    private float timer;
    private Vector3 pos;
    public GameObject cam;
    public GameObject visual;
    public GameObject pointYCamera;
    public GameObject[] ailes;
    // Start is called before the first frame update
    void Awake()
    {
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



        visual.transform.position = Vector3.Lerp(visual.transform.position,dataController.destination,Time.deltaTime*100f);

        Camera();
        Character(); 
        CharacterGlide(); 
    }
    void InitState()
    {
        IPlayerStateArray.Add(new CS_Move_R());
        IPlayerStateArray.Add(new CS_Jump());
        IPlayerStateArray.Add(new CS_Fall());
        IPlayerStateArray.Add(new CS_Glide());
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
            IPlayerStateArray[(int)dataController.CurrentStates].ExitState(ref dataController);
            dataController.CurrentStates = dataController.TargetStates;
            IPlayerStateArray[(int)dataController.CurrentStates].EnterState(ref dataController);
            dataController.ChangeState = false;
        }
    }






    void Camera()
    {
        dataCamera.direction_cam = cam.transform.eulerAngles;
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

    void Character()
    {
        if (dataController.destination - visual.transform.position != Vector3.zero && InputManager.GetInputMove() != Vector2.zero)
        {
            visual.transform.rotation = Quaternion.Lerp(visual.transform.rotation, Quaternion.LookRotation(dataController.destination - visual.transform.position, transform.up), Time.deltaTime * 10f);
        }
    }
    void CharacterGlide()
    {
        if(dataController.CurrentStates == States.glide)
        {
            ailes[0].transform.localRotation = Quaternion.Lerp(ailes[0].transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime);
            ailes[1].transform.localRotation = Quaternion.Lerp(ailes[1].transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime);
        }
        else
        {
            ailes[0].transform.localRotation = Quaternion.Lerp(ailes[0].transform.localRotation ,Quaternion.Euler(0, 0, 50),Time.deltaTime);
            ailes[1].transform.localRotation = Quaternion.Lerp(ailes[1].transform.localRotation, Quaternion.Euler(0, 0, -50), Time.deltaTime);
        }
    }
}
