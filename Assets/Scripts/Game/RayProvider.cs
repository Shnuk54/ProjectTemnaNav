using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayProvider : MonoBehaviour,IRayProvider
{
    Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
    }
   public Ray CreateRay(){
       return _camera.ScreenPointToRay(ScreenCenter());
   }

    private Vector3 ScreenCenter(){
        return new Vector3(_camera.pixelWidth/2,_camera.pixelHeight/2,0);
    }
}
