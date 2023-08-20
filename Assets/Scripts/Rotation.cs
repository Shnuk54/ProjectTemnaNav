using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float _xSpeed;
    [SerializeField] private float _ySpeed;
    [SerializeField] private float _zSpeed;

    private Transform _transform;

    private void FixedUpdate()
    {
        Rotate();
    }
    private void Rotate()
    {
        _transform.Rotate(_xSpeed, _ySpeed, _zSpeed);
    }
}
