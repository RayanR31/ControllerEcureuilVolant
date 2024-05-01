using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static DataController;

public class CS_Jump : IPlayerState
{
    public GS_Jump changeStateMove = new GS_Jump();
    private float currentGravity;
    float t = 0 ;
    public void EnterState(ref StructController _dataController)
    {
        t = 0;
        //_dataController.direction = Vector3.zero;
        //InputManager.CancelInputJump();
    }

    public void CurrentStateUpdate(ref StructController _dataController, StructCamera _dataCamera, DataScriptableObject _data)
    {
        PhysicsCustom.CalculNormal(ref _dataController, _dataCamera, 1f);
        Vector3 input = new Vector3(InputManager.GetInputMove().x , 0, InputManager.GetInputMove().y ) * _data.pourcentageMagnitude;
        _dataController.direction = Vector3.Lerp(_dataController.direction, input, Time.fixedDeltaTime * _data.angularDrag);

        GestionGravityY(_data);

        _dataController.destination += _dataController.Controller_go.transform.rotation * new Vector3(_dataController.direction.x, currentGravity * _data.curve.Evaluate(ratioT()), _dataController.direction.z) * Time.deltaTime; 
        if (t > 0.3f)
        {
           // PhysicsCustom.CheckWall(ref _dataController);
        }
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
            InputManager.GetInputGlide();
        }
    }

    public void ChangeStateByNature(ref StructController _dataController, StructCamera _dataCamera)
    {
        CancelJump(ref _dataController);
    }
    void CancelJump(ref StructController _dataController)
    {
        if(t > 1f)
        {
            _dataController.TargetStates = States.fall;
            _dataController.ChangeState = true;
        }
    }
    void GestionGravityY(DataScriptableObject _data)
    {
        currentGravity = _data.gravity - (InputManager.GetInputMagnitude() * _data.temp);
    }
    float ratioT()
    {
        if(t < 1) t += Time.deltaTime * 5.5f;

        return t; 
    }
}
