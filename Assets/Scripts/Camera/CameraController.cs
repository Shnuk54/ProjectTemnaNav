using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, Assets.Scripts.ICameraController
{
    [SerializeField] private Camera _cinameCamera;
    [SerializeField] private Camera _firsPersonCamera;
    private Transform _transform;
    [SerializeField] private Vector3 _offset;
    [SerializeField] Transform _target;
    [SerializeField] float changeSpeed = 2f;
    [SerializeField] float changePositionSpeed = 0.1f;
    [SerializeField] bool _isMoving;
   private void Start()
   {
        _cinameCamera = Camera.main;
        Camera.SetupCurrent(_cinameCamera);
        _transform = GetComponent<Transform>();
        _target = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        _firsPersonCamera = Camera.main;
       
   }

    public Transform Target
    {
        get { return _target; }
        set { _target = value;}
    }
    public bool IsMoving
    {
        get { return _isMoving; }
    }
   
    public Transform CamTransform
    {
        get { return _transform; }
    }
    private void Update()
    {
       FollowTarget(_target);
       SwitchCamera(CameraStateHandler.instance.State);
    }
    public void FollowTarget(Transform target)
    {
        _transform.LookAt(target);
      
    }

    public void SwitchCamera(CameraStates state)
    {
        if (state.FirstPerson)
        {
            _cinameCamera.enabled = false;
            _firsPersonCamera.enabled = true;
        }
        if(state.Cinematic)
        {
            _cinameCamera.enabled = true;
            _firsPersonCamera.enabled = false;
        }
    }

    public void ChangePosition(Transform newPos)
    {
        if (_isMoving) return;
        StartCoroutine("SmoothPositionChange",newPos); 
    }
    public void ChangePosition(List<Vector3> path)
    {
        if (_isMoving) return;
        StartCoroutine("SmoothPositionChange", path);
    }

    private  IEnumerator SmoothPositionChange(Transform newPos)
    {
        while (_transform.position != newPos.position)
        {
            if (changeSpeed <= 0) break;
            _isMoving = true;
            yield return new WaitForSeconds(0.001f);
            _transform.position = Vector3.MoveTowards(_transform.position, newPos.position, changePositionSpeed * Time.deltaTime);
            _transform.LookAt(_target);
        }
        _isMoving = false;
        yield return null;
    }
    private IEnumerator SmoothPositionChange(List<Vector3> path)
    {
        
        foreach(Vector3 pos in path)
        {
            while (_transform.position != pos)
            {
                if (changeSpeed <= 0) break;
                _isMoving = true;
                _transform.position = Vector3.MoveTowards(_transform.position, pos,changeSpeed * Time.deltaTime);
                _transform.LookAt(_target);
                yield return null;
            }
        }
        
        _isMoving = false;
    }
}
