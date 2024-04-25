using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using static DataController;

public class CS_Move_R : IPlayerState
{
    public GS_Move changeStateMove = new GS_Move();

    public void EnterState(StructController _dataController)
    {
    }

    public void CurrentStateUpdate(ref StructController _dataController, StructCamera _dataCamera , DataScriptableObject _data)
    {
         _dataController.destination = Vector3.Slerp(_dataController.destination,
             PhysicsCustom.SearchPosition(ref _dataController.Controller_go, InputManager.inputMove.x, 1, InputManager.inputMove.y, Color.red),Time.deltaTime * 6f);
       /* _dataController.destination = Vector3.Slerp(_dataController.destination,
             PhysicsCustom.SearchPosition(ref _dataController.Controller_go,0, 1, 1, Color.red), Time.deltaTime * 6f);*/
    }

    public void ExitState(StructController _dataController)
    {
    }

    public void ChangeStateByInput(ref StructController _dataController)
    {

    }

    public void ChangeStateByNature(ref StructController _dataController, StructCamera _dataCamera)
    {

    }
}
