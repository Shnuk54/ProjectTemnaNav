using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleSensetiveObject : MonoBehaviour
{
    private void MoveToRandomPos()
    {
        
        transform.LookAt(FindObjectOfType<PlayerController>().transform);
    }
    private void OnEnable()
    {
        Events.onPlayerBlink += MoveToRandomPos;
    }
    private void OnDisable()
    {
        Events.onPlayerBlink -= MoveToRandomPos;
    }
}
