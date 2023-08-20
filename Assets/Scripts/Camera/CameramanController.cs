using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class CameramanController : MonoBehaviour
{
    [SerializeField] private List<ICameraPosition> _camPositions = new List<ICameraPosition>();
    [SerializeField] private float _maxFollowDistance;
    [SerializeField] private ICameraPosition _nearestPos;
    [SerializeField] private float _distance;
    [SerializeField] private bool _farFromPositions = false;
    [SerializeField] private bool _behindPlayer = false;
    private Transform _playerCameraPos;
    private Assets.Scripts.ICameraController _cameraController;
    private LastPath _followPath;
   
    private void Start()
    {
        _cameraController = GetComponent<Assets.Scripts.ICameraController>();
        _followPath = FindObjectOfType<PlayerMovement>().GetComponent<LastPath>();
        FindCameraPositions();
        
    }

    private void FindCameraPositions()
    {
        var objects = GameObject.FindGameObjectsWithTag("CameraPosition");
        _playerCameraPos = GameObject.FindGameObjectWithTag("PlayerCameraPosition").GetComponent<Transform>();
        foreach (GameObject obj in objects)
        {
            if(obj.GetComponent<ICameraPosition>() != null)
            {
                _camPositions.Add(obj.GetComponent<ICameraPosition>());
                _nearestPos = obj.GetComponent<ICameraPosition>();
            }
           
        }
        if(_camPositions.Count == 0)
        {
            CameraStateHandler.instance.FirstPersonCam = true;
        }
        else
        {
            FindNearestPosition();
        }
    }
    private void Update()
    {
        if(_camPositions.Count != 0)
        {
            ChangeCameraPosition();
        }

       
    }
    private void ChangeCameraPosition()
    {
        _distance = _nearestPos.Distance;
        OutOfViewCheck();
        FindNearestPosition();
        if(_farFromPositions && _followPath.PathTracking == false && _cameraController.IsMoving == false)
        {
             
                _followPath.ResetPath();
                _followPath.SaveLastPath(true);
        }
        
       
    }
    private void OutOfViewCheck()
    {
        bool far = false; 
        for (int i = 0; i <= _camPositions.Count - 1; i++)
        {
            if (_camPositions[i].CanSeeTarget == false)
            {
                far = true;
               
            }
            else
            {
                if (_followPath.PathTracking == true)
                {
                    _cameraController.ChangePosition(_followPath.Path);
                    //CameraStateHandler.instance.State.Cinematic = false ;
                } 
                far = false;
                _followPath.SaveLastPath(false);
                break;
            }
        }
        _farFromPositions = far;
       
    }
    private void FindNearestPosition()
    {
        if (_farFromPositions || InputHandler.instance.aiming)
        {
            //  _cameraController.ChangePosition(_playerCameraPos);
          
            CameraStateHandler.instance.FirstPersonCam = true;
            _behindPlayer = true;
            return;
        }
     
        float nearestDistance = 0;
        float secondDistance = 0;
        for (int i = 0; i <= _camPositions.Count - 1; i++)
        {
            nearestDistance = _nearestPos.Distance;
            secondDistance = _camPositions[i].Distance;
            if (nearestDistance > secondDistance && _camPositions[i].CanSeeTarget)
            {
                _nearestPos = _camPositions[i];
                _behindPlayer = false;
            }
            if (_nearestPos.CanSeeTarget == false && nearestDistance < secondDistance && _camPositions[i].CanSeeTarget)
            {
                _nearestPos = _camPositions[i];
                _behindPlayer = false;

            }
            CameraStateHandler.instance.FirstPersonCam = false;
            _cameraController.ChangePosition(_nearestPos.PosTransform);
        }
    }

}
