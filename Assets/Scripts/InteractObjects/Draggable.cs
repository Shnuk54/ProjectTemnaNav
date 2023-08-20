using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : DraggableObject,IDragable
{
    // Start is called before the first frame update
    void Start()
    {
        canThrow = true;
        canGrab = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleDrag();
    }
}
