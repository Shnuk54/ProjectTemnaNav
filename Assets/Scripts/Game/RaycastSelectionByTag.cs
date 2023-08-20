using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSelectionByTag : MonoBehaviour,IRaySelector
{

[SerializeField] private Transform _selection;
[SerializeField] private Transform _startRayPos;
[SerializeField] private string _selectionTag;
[SerializeField] private float _selectionDistance;

[SerializeField] private float _distance;
private void Start()
{
    _startRayPos = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        _selection = null;
    }
   public void Check(Ray ray){

      

        if(Physics.Raycast(ray,out var hit)){

            var selection = hit.transform;

            _distance = Vector3.Distance(selection.position,_startRayPos.position);

            if(selection.tag == _selectionTag &&  _distance <= _selectionDistance){

                _selection = selection;

                PlayerStateHandler.instance.IsSeeItem = true;

               
            }
            else
            {
                PlayerStateHandler.instance.IsSeeItem = false;
                _selection = null;
            }


        }
        else
        {
            _selection = null;
            PlayerStateHandler.instance.IsSeeItem = false;
        }
   }

   public Transform GetSelection(){
       return _selection;
   }
}
