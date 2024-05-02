using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataController;
using static UnityEditor.PlayerSettings;

public class CameraMain : MonoBehaviour
{

    [SerializeField] public StructCamera dataCamera = new StructCamera();


    [Header("TEMPORARY - Camera - Character")]
    private float timer;
    private Vector3 pos;
    public GameObject cam;
    public GameObject visual;
    public GameObject pointYCamera;
    public GameObject[] ailes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        visual.transform.position = Vector3.Lerp(visual.transform.position, GameManager.instance.GetControllerData().destination, Time.deltaTime * 100f);

            
        Camera();
        Character();
        CharacterGlide();

        GameManager.instance.SetCameraData(dataCamera);

    }


    void Camera()
    {
        dataCamera.direction_cam = cam.transform.eulerAngles;
        if (GameManager.instance.GetControllerData().CurrentStates == States.move || timer > 0.3f)
        {
            pointYCamera.transform.position = Vector3.Lerp(pointYCamera.transform.position, GameManager.instance.GetControllerData().destination, Time.deltaTime * 3f);
            pos = pointYCamera.transform.position;
            if (GameManager.instance.GetControllerData().CurrentStates == States.move) timer = 0;

        }
        else
        {
            timer += Time.deltaTime;
            pos = new Vector3(GameManager.instance.GetControllerData().destination.x, pointYCamera.transform.position.y, GameManager.instance.GetControllerData().destination.z);
            pointYCamera.transform.position = Vector3.Lerp(pointYCamera.transform.position, pos, Time.deltaTime * 3f);
        }
    }

    void Character()
    {
        if (GameManager.instance.GetControllerData().destination - visual.transform.position != Vector3.zero && InputManager.GetInputMove() != Vector2.zero)
        {
            visual.transform.rotation = Quaternion.Lerp(visual.transform.rotation, Quaternion.LookRotation(GameManager.instance.GetControllerData().destination - visual.transform.position, transform.up), Time.deltaTime * 10f);
        }
    }
    void CharacterGlide()
    {
        if (GameManager.instance.GetControllerData().CurrentStates == States.glide)
        {
            ailes[0].transform.localRotation = Quaternion.Lerp(ailes[0].transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime);
            ailes[1].transform.localRotation = Quaternion.Lerp(ailes[1].transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime);
        }
        else
        {
            ailes[0].transform.localRotation = Quaternion.Lerp(ailes[0].transform.localRotation, Quaternion.Euler(0, 0, 50), Time.deltaTime);
            ailes[1].transform.localRotation = Quaternion.Lerp(ailes[1].transform.localRotation, Quaternion.Euler(0, 0, -50), Time.deltaTime);
        }
    }
}
