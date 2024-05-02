using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataController;

public class ControllerMain : MonoBehaviour
{

    [SerializeField] private StructController dataController = new StructController();

    [SerializeField] private List<IPlayerState> IPlayerStateArray = new List<IPlayerState>();

    [SerializeField] private List<DataScriptableObject> DataScriptableObjectList = new List<DataScriptableObject>();


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

        GameManager.instance.SetControllerData(dataController); 
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
        IPlayerStateArray[(int)dataController.CurrentStates].CurrentStateUpdate(ref dataController, GameManager.instance.GetCameraData() , DataScriptableObjectList[(int)dataController.CurrentStates]);
        IPlayerStateArray[(int)dataController.CurrentStates].ChangeStateByInput(ref dataController);
        IPlayerStateArray[(int)dataController.CurrentStates].ChangeStateByNature(ref dataController, GameManager.instance.GetCameraData());
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
}
