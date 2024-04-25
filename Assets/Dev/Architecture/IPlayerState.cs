using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataController;

public interface IPlayerState
{
    public void EnterState(StructController _dataController);

    public void CurrentStateUpdate(ref StructController _dataController, StructCamera _dataCamera, DataScriptableObject _data);

    public void ExitState(StructController _dataController);

    public void ChangeStateByInput(ref StructController _dataController);

    public void ChangeStateByNature(ref StructController _dataController, StructCamera _dataCamera);

}
