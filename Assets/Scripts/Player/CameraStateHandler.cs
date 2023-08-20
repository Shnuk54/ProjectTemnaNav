using System.Collections;
using UnityEngine;


    public class CameraStateHandler : MonoBehaviour
    {
    [SerializeField] CameraStates _cameraState = new CameraStates();
    public static CameraStateHandler instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public CameraStates State
    {
        get { return _cameraState; }
       
    }
    public bool FirstPersonCam
    {
        set {  _cameraState.FirstPerson = value; }

    }
    private void Update()
    {
        UpdateState();
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void UpdateState()
    {
       if(_cameraState.FirstPerson == false)
        {
            _cameraState.Cinematic = true;
        }
        else
        {
            _cameraState.Cinematic = false;
        }
        
      //  _cameraState.FirstPerson = InputHandler.instance.looking;
    }

}
