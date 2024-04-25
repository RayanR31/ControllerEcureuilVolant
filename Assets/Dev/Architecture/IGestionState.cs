using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataController;

public interface IGestionState
{
    void Check(ref StructController _dataController);
    void CheckWall(ref StructController _dataController, ref float speed);
}
