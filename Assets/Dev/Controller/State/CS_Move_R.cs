using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using static DataController;

public class CS_Move_R : IPlayerState
{
    public GS_Move changeStateMove = new GS_Move();
    Vector3 direction;
    public void EnterState(StructController _dataController)
    {
        direction = _dataController.Controller_go.transform.position;
    }

    public void CurrentStateUpdate(ref StructController _dataController, StructCamera _dataCamera , DataScriptableObject _data)
    {
        // Calculer la normale une seule fois avant les appels à SearchPosition
        PhysicsCustom.CalculNormal(_dataController.Controller_go, _dataCamera);

        // Stocker le résultat de SearchPosition dans une variable locale
        // Vector3 newPosition = PhysicsCustom.SearchPosition(ref _dataController.Controller_go, _dataCamera, InputManager.inputMove.x, 1, InputManager.inputMove.y, Color.red);

        // Utiliser la nouvelle position pour mettre à jour la destination
        /*_dataController.destination = Vector3.Slerp(_dataController.destination,
            new Vector3(newPosition.x, newPosition.y, newPosition.z),
            Time.deltaTime * 6f);*/
        Debug.DrawRay(_dataController.Controller_go.transform.position, _dataController.Controller_go.transform.forward * 5f, Color.green);
        direction += _dataController.Controller_go.transform.rotation * new Vector3(InputManager.GetInputMove().x, 0, InputManager.GetInputMove().y);
        _dataController.destination = direction * 5f * Time.deltaTime;
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
