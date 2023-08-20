using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Lockable))]
public class Door : DraggableObject,IDragable
{
   
    private ILockable keyLock;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        keyLock = GetComponent<ILockable>();
        canGrab = true;
        canThrow = false;
        freezeRot = false;
    }
   
    private void Update()
    {
       
        if(keyLock.IsLocked == false)
        {
            canGrab = true;
            HandleDrag();
        }
        else
        {
            canGrab = false;
        }
    }

  
}
