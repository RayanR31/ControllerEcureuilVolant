using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataController;

public class GameManager : MonoBehaviour
{
    #region SINGLETON PATTERN
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    [SerializeField] public StructController dataController = new StructController();
    [SerializeField] public StructCamera dataCamera = new StructCamera(); 

    public void SetControllerData(StructController _dataController)
    {
        dataController = _dataController;
    }
    public StructController GetControllerData()
    {
        return dataController;
    }
    public void SetCameraData(StructCamera _dataCamera)
    {
        dataCamera = _dataCamera;
    }
    public StructCamera GetCameraData()
    {
        return dataCamera;
    }
}
