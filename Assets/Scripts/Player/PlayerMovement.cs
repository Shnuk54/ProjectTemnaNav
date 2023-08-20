using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    
    [Header("Walk and Sprint")]
    [Range(0.01f, 10f)] [SerializeField] float _speed;
    [Range(0.02f, 15f)] [SerializeField] float _sprintSpeed;
    [Range(-10, 10)] [SerializeField] float _gravity = -5;
    CharacterController _charController;
    private Transform _transform;

    [Header("Steps")]
    [SerializeField] private float _stepLengthWalk;
    [SerializeField] private float _stepLengthSprint;
    [SerializeField] private bool _leftStep;
    [SerializeField] private bool _rightStep;
    [SerializeField] AudioClip _stepSound;

    [SerializeField] AudioSource _LAudio;
    [SerializeField] AudioSource _RAudio;
    private Vector3 _currentStepPos;
   

    [Header("Head")]

    [Range(0,85)] [SerializeField] float _maxXRot;
    [Range(-85, 0)] [SerializeField] float _minXRot;
    [Range(0, 85)] [SerializeField] float _maxYRot;
    [Range(-85, 0)] [SerializeField] float _minYRot;
    [Range(0.5f, 10)] [SerializeField] float _mouseSens;
    [SerializeField] Transform _headTransform;
    private float _xRot;
    private float _yHeadRot;
    private float _yBodyRot;
    
   
   

   
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
        _currentStepPos = this.transform.position;
        
    }

    private void Update()
    {
        HandleStep();
       
    }

   
    public void Move(){
        var verInput = InputHandler.instance.verInput;
        var horInput = InputHandler.instance.horInput;

        var forward = _headTransform.forward.normalized;
        var right = _headTransform.right.normalized;

        forward.y = 0;
        right.y = 0;

        Vector3 dir;
        dir =  (forward*verInput + right*horInput)/100;

        if(InputHandler.instance.sprinting == false)dir = dir * _speed;

        if (InputHandler.instance.sprinting) dir = dir * _sprintSpeed;

        dir.y = _gravity;

        _charController.Move(dir);
    }
     public void HeadRotation(){
        _yHeadRot += InputHandler.instance.mouseX * _mouseSens;
        _yBodyRot += InputHandler.instance.mouseX * _mouseSens;

        _xRot -= InputHandler.instance.mouseY * _mouseSens;
        _xRot = Mathf.Clamp(_xRot, _minXRot, _maxXRot);

        _yHeadRot = Mathf.Clamp(_yHeadRot, _minYRot, _maxYRot);
        _headTransform.localRotation = Quaternion.Euler(_xRot, _yHeadRot, 0);
    }
    public void BodyRotatio()
    {
        
     _transform.localRotation = Quaternion.Euler(0, _yBodyRot, 0);
        
    }
    private void HandleStep()
    {
        if(Vector3.Distance(_currentStepPos,_transform.position) > _stepLengthWalk)
        {
            _currentStepPos = _transform.position;

            if (!_leftStep && !_rightStep) _leftStep = true;
            if (_leftStep)
            {
                _leftStep = false;
                _rightStep = true;
                _RAudio.clip = _stepSound;
                _RAudio.panStereo = 0.9f;
                _RAudio.Play();
                return;
            }
            if (_rightStep)
            {
                _leftStep = true;
                _rightStep = false;
                _LAudio.clip = _stepSound;
                _LAudio.panStereo = -0.9f;
                _LAudio.Play();
                return;
            }
        }
    }
}
