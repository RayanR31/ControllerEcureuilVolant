using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataController;

public class CS_Fall : IPlayerState
{
    public GS_Jump changeStateMove = new GS_Jump();
    float currentGravity; 
    public void EnterState(ref StructController _dataController)
    {
        _dataController.direction = Vector3.zero;

        InputManager.CancelInputJump();

    }

    public void CurrentStateUpdate(ref StructController _dataController, StructCamera _dataCamera, DataScriptableObject _data)
    {

        _dataController.currentSpeed = Mathf.Lerp(_dataController.currentSpeed, _data.speed * InputManager.GetInputMagnitude(), Time.deltaTime * 5f);

        Vector3 input = new Vector3(InputManager.GetInputMove().x, 0, InputManager.GetInputMove().y);
        _dataController.Controller_go.transform.rotation = Quaternion.Lerp(_dataController.Controller_go.transform.rotation,Quaternion.Euler(_dataCamera.direction_cam),Time.deltaTime);
        _dataController.direction = Vector3.Slerp(_dataController.direction, _dataController.Controller_go.transform.rotation * input, Time.fixedDeltaTime * _data.angularDrag);

        _dataController.destination += _dataController.direction * _dataController.currentSpeed * Time.fixedDeltaTime;


        _dataController.destination -= new Vector3(0, _data.curve.Evaluate(Time.time), 0) * _data.gravity * Time.fixedDeltaTime; 

        PhysicsCustom.CheckWall(ref _dataController);


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
}
