using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using static DataController;

public class CS_Move_R : IPlayerState
{
    public GS_Move changeStateMove = new GS_Move();
    private Vector3 velocity;
        
    public void EnterState(ref StructController _dataController)
    {
        //_dataController.direction = _dataController.Controller_go.transform.position;
    }
    public void CurrentStateUpdate(ref StructController _dataController, StructCamera _dataCamera , DataScriptableObject _data)
    {
        PhysicsCustom.CalculNormal(ref _dataController, _dataCamera , 1f);

        // Debug.DrawRay(_dataController.Controller_go.transform.position, _dataController.Controller_go.transform.forward * 5f, Color.green);
        // Debug.DrawRay(_dataController.Controller_go.transform.position, _dataController.Controller_go.transform.up * 5f, Color.red);
        //PhysicsCustom.AdjustAxeY(ref _dataController);
        //_dataController.direction += _dataController.Controller_go.transform.rotation * new Vector3(InputManager.GetInputMove().x, 0, InputManager.GetInputMove().y);
        // _dataController.destination = _dataController.direction * _data.speed * Time.deltaTime;

        velocity = Vector3.Slerp(velocity , new Vector3(InputManager.GetInputMove().x, InputManager.GetInputMove().y, 0),Time.deltaTime); // AngularDrag

        _dataController.destination = 
            Vector3.Lerp(_dataController.destination,PhysicsCustom.SearchPosition(ref _dataController, _dataCamera, velocity.x, 1, velocity.y),Time.deltaTime * 3f);
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
