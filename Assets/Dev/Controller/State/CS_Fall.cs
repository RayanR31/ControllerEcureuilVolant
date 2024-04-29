using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataController;

public class CS_Fall : IPlayerState
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

        /*   Debug.DrawRay(_dataController.Controller_go.transform.position, _dataController.Controller_go.transform.up * 5f, Color.red);
           Debug.Log(timer);
           _dataController.direction += _dataController.Controller_go.transform.rotation * new Vector3(InputManager.GetInputMove().x, 1, InputManager.GetInputMove().y);
           _dataController.destination = _dataController.direction * 5f * Time.deltaTime;*/

        PhysicsCustom.Check(ref _dataController);
        //Debug.DrawRay(_dataController.destination, _dataController.Controller_go.transform.rotation * new Vector3(InputManager.GetInputMove().x, -1, InputManager.GetInputMove().y) * 2f, Color.yellow);

        _dataController.direction += _dataController.Controller_go.transform.rotation * new Vector3(InputManager.GetInputMove().x,-1, InputManager.GetInputMove().y);
        _dataController.destination = _dataController.direction  * _data.speed * Time.deltaTime;
        //_dataController.destination.y += -5f * Time.deltaTime;
    }

    public void ExitState(ref StructController _dataController)
    {
       // _dataController.direction = _dataController.Controller_go.transform.position;
    }

    public void ChangeStateByInput(ref StructController _dataController)
    {

    }

    public void ChangeStateByNature(ref StructController _dataController, StructCamera _dataCamera)
    {

    }
    void CancelJump(ref StructController _dataController)
    {

    }
}
