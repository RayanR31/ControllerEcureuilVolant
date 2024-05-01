using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using static DataController;

public class CS_Move_R : IPlayerState
{
    public GS_Move changeStateMove = new GS_Move();
        
    public void EnterState(ref StructController _dataController)
    {
        InputManager.CancelInputJump();
    }
    public void CurrentStateUpdate(ref StructController _dataController, StructCamera _dataCamera , DataScriptableObject _data)
    {
        PhysicsCustom.CalculNormal(ref _dataController, _dataCamera , 1f);
        _dataController.currentSpeed = Mathf.Lerp(_dataController.currentSpeed, _data.speed * InputManager.GetInputMagnitude(), Time.deltaTime * 5f);

        _dataController.currentVelocity = Vector3.Slerp(_dataController.currentVelocity, new Vector3(InputManager.GetInputMove().x, InputManager.GetInputMove().y, 0),Time.deltaTime * _data.angularDrag); // AngularDrag

        _dataController.destination = 
            Vector3.Lerp(_dataController.destination,PhysicsCustom.SearchPosition(ref _dataController, _dataCamera, _dataController.currentVelocity.x, 1, _dataController.currentVelocity.y),Time.deltaTime * _dataController.currentSpeed);
    }


    public void ExitState(ref StructController _dataController)
    {
    }

    public void ChangeStateByInput(ref StructController _dataController)
    {
        if(InputManager.GetInputJump() == true)
        {
            _dataController.TargetStates = States.jump;
            _dataController.ChangeState = true;
            InputManager.CancelInputJump();
        }
    }

    public void ChangeStateByNature(ref StructController _dataController, StructCamera _dataCamera)
    {

    }


}
