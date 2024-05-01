using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataController;

public class CS_Fall : IPlayerState
{
    public GS_Jump changeStateMove = new GS_Jump();
    float currentGravity;
    float t = 0;
    public void EnterState(ref StructController _dataController)
    {
        //_dataController.direction = Vector3.zero;

        InputManager.CancelInputJump();
        t = 0;

    }

    public void CurrentStateUpdate(ref StructController _dataController, StructCamera _dataCamera, DataScriptableObject _data)
    {

        /* _dataController.currentSpeed = Mathf.Lerp(_dataController.currentSpeed, _data.speed * InputManager.GetInputMagnitude(), Time.deltaTime * 5f);

         Vector3 input = new Vector3(InputManager.GetInputMove().x, 0, InputManager.GetInputMove().y) ;
         _dataController.Controller_go.transform.rotation = Quaternion.Lerp(_dataController.Controller_go.transform.rotation,Quaternion.Euler(_dataCamera.direction_cam),Time.deltaTime);

         _dataController.direction = Vector3.Slerp(_dataController.direction, _dataController.Controller_go.transform.rotation * input, Time.fixedDeltaTime * _data.angularDrag);

         _dataController.destination += _dataController.direction * _dataController.currentSpeed * Time.fixedDeltaTime;


         _dataController.destination -= new Vector3(0, _data.curve.Evaluate(ratioT()), 0) * _data.gravity * Time.fixedDeltaTime;*/
        // Debug.Log(new Vector3(0, _data.curve.Evaluate(ratioT()), 0) * _data.gravity * Time.fixedDeltaTime);

        _dataController.Controller_go.transform.rotation = Quaternion.Lerp(_dataController.Controller_go.transform.rotation, Quaternion.Euler(_dataCamera.direction_cam), Time.deltaTime);

        Vector3 input = new Vector3(InputManager.GetInputMove().x, 0, InputManager.GetInputMove().y) * _data.pourcentageMagnitude;
        _dataController.direction = Vector3.Lerp(_dataController.direction, input, Time.fixedDeltaTime * _data.angularDrag);


        _dataController.destination += _dataController.Controller_go.transform.rotation * new Vector3(_dataController.direction.x, 0, _dataController.direction.z) * Time.deltaTime;
        _dataController.destination += new Vector3(0, -_data.gravity * _data.curve.Evaluate(ratioT()),0) * Time.deltaTime;

        PhysicsCustom.CheckWall(ref _dataController);


    }

    public void ExitState(ref StructController _dataController)
    {
        //_dataController.direction = Vector3.zero;
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

    }
    void GestionGravity(ref StructController _dataController , DataScriptableObject _data)
    {
        currentGravity = Mathf.Lerp(currentGravity, _data.gravity, Time.deltaTime); 
    }
    float ratioT()
    {
        if (t > 0) t += Time.deltaTime * 3;

        return t;
    }
}
