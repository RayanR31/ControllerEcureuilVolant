using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static DataController;

public class CS_Jump : IPlayerState
{
    public GS_Jump changeStateMove = new GS_Jump();
    private float timerCancelJump;
    private float currentGravity; 
    public void EnterState(ref StructController _dataController)
    {
        timerCancelJump = 0; 
        //InputManager.CancelInputJump();
    }

    public void CurrentStateUpdate(ref StructController _dataController, StructCamera _dataCamera, DataScriptableObject _data)
    {
        PhysicsCustom.CalculNormal(ref _dataController, _dataCamera, 1f);
        Vector3 input = new Vector3(InputManager.GetInputMove().x , 0, InputManager.GetInputMove().y ) * _data.pourcentageMagnitude;
        _dataController.direction = Vector3.Slerp(_dataController.direction, _dataController.Controller_go.transform.rotation * input, Time.fixedDeltaTime * _data.angularDrag);

        GestionGravityY(_data);
        _dataController.direction += _dataController.Controller_go.transform.rotation * new Vector3(0, currentGravity * _data.curve.Evaluate(-Time.time) * Time.deltaTime, 0);


        _dataController.destination += _dataController.direction * _data.speed * Time.fixedDeltaTime;

        if (timerCancelJump > 0.3f)
        {
            PhysicsCustom.CheckWall(ref _dataController);
        }
    }

    public void ExitState(ref StructController _dataController)
    {
        _dataController.direction = Vector3.zero; 
    }

    public void ChangeStateByInput(ref StructController _dataController)
    {
        if (InputManager.GetInputGlide() == true)
        {
            _dataController.TargetStates = States.glide;
            _dataController.ChangeState = true;
            InputManager.GetInputGlide();
        }
    }

    public void ChangeStateByNature(ref StructController _dataController, StructCamera _dataCamera)
    {
        timerCancelJump += Time.deltaTime;    
        CancelJump(ref _dataController);
    }
    void CancelJump(ref StructController _dataController)
    {
        if(timerCancelJump > 0.5f)
        {
            _dataController.TargetStates = States.fall;
            _dataController.ChangeState = true;
        }
    }
    void GestionGravityY(DataScriptableObject _data)
    {
        currentGravity = _data.gravity - (InputManager.GetInputMagnitude() * _data.temp);
    }
}
