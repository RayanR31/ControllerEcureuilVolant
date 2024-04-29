using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataController;

public class CS_Jump : IPlayerState
{
    public GS_Jump changeStateMove = new GS_Jump();
    float timer; 
    public void EnterState(ref StructController _dataController)
    {
        timer = 0; 
        InputManager.CancelInputJump();
        //StartCoroutine(CancelJump());
    }

    public void CurrentStateUpdate(ref StructController _dataController, StructCamera _dataCamera, DataScriptableObject _data)
    {

      //  Debug.DrawRay(_dataController.Controller_go.transform.position, _dataController.Controller_go.transform.up * 5f, Color.red);
        _dataController.direction += _dataController.Controller_go.transform.rotation * new Vector3(InputManager.GetInputMove().x, 1, InputManager.GetInputMove().y);
        _dataController.destination = _dataController.direction * _data.speed * Time.deltaTime;
    }

    public void ExitState(ref StructController _dataController)
    {
    }

    public void ChangeStateByInput(ref StructController _dataController)
    {

    }

    public void ChangeStateByNature(ref StructController _dataController, StructCamera _dataCamera)
    {
        timer += Time.deltaTime;    
        CancelJump(ref _dataController);
    }
    void CancelJump(ref StructController _dataController)
    {
        if(timer > 0.5f)
        {
            _dataController.TargetStates = States.fall;
            _dataController.ChangeState = true;
        }
    }
}
