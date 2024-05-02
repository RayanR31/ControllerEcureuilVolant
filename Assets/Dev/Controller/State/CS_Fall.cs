using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataController;

public class CS_Fall : IPlayerState
{
    public GS_Jump changeStateMove = new GS_Jump();
    float t = 0;
    public void EnterState(ref StructController _dataController)
    {
        InputManager.CancelInputJump();
        t = 0;
    }

    public void CurrentStateUpdate(ref StructController _dataController, StructCamera _dataCamera, DataScriptableObject _data)
    {

        _dataController.Controller_go.transform.rotation = Quaternion.Lerp(_dataController.Controller_go.transform.rotation, Quaternion.Euler(_dataCamera.direction_cam), Time.deltaTime);

        Vector3 input = new Vector3(InputManager.GetInputMove().x, 0, InputManager.GetInputMove().y) * _data.pourcentageMagnitude;
        _dataController.direction = Vector3.Lerp(_dataController.direction, input, Time.fixedDeltaTime * _data.angularDrag);


        _dataController.destination += _dataController.Controller_go.transform.rotation * new Vector3(_dataController.direction.x, 0, _dataController.direction.z) * Time.deltaTime;
        _dataController.destination += new Vector3(0, -_data.gravity * _data.curve.Evaluate(ratioT()),0) * Time.deltaTime;

    }

    public void ExitState(ref StructController _dataController)
    {
        t = 0;
    }

    public void ChangeStateByInput(ref StructController _dataController)
    {
        if (InputManager.GetInputGlide() == true)
        {
            _dataController.TargetStates = States.glide;
            _dataController.ChangeState = true;
            InputManager.CancelInputGlide();
        }
    }

    public void ChangeStateByNature(ref StructController _dataController, StructCamera _dataCamera)
    {
        if (PhysicsCustom.CheckWall(ref _dataController,0.5f))
        {
            _dataController.TargetStates = States.move;
            _dataController.ChangeState = true;
        }
    }
    float ratioT()
    {
        if (t > 0) t += Time.deltaTime * 3;

        return t;
    }
}
