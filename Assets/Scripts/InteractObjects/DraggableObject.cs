using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DraggableObject : MonoBehaviour,IDragable
{

  


    [SerializeField] protected float lastMoveDir;
    [SerializeField] protected bool isGrabed;
    [SerializeField] protected bool canGrab = true;
    [SerializeField] protected float throwForce = 2f;
    [SerializeField] protected bool canThrow = true;
    [SerializeField] protected bool freezeRot = true;
    protected Ray playerAim;

    protected Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
   

    public void Throw()
    {
        if(isGrabed && InputHandler.instance.aiming && canThrow)
        {
            _rb.AddForce(playerAim.direction * throwForce, ForceMode.Impulse);
            isGrabed = false;
            StartCoroutine("GrabDelay");
           
        }
    }
       
    public void Grab()
    {
        if (canGrab)
        {
        isGrabed = true;
        PlayerStateHandler.instance.IsHoldingItem = true;
        StartCoroutine("Dragging");
        }
    }
    protected void HandleDrag()
    {
        playerAim = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Throw();
    }

   protected IEnumerator Dragging()
    {
        while (isGrabed && InputHandler.instance.shooting)
        {
            Vector3 nextPos = Camera.main.transform.position + playerAim.direction * 2f;
            Vector3 currPos = transform.position;
            _rb.velocity = (nextPos - currPos) * 10f;
            
                _rb.freezeRotation = freezeRot;
            
            yield return new WaitForSeconds(0.01f);
        }
        _rb.freezeRotation = false;
        PlayerStateHandler.instance.IsHoldingItem = false;
        StopCoroutine("Dragging");
    }
    protected IEnumerator GrabDelay()
    {
        canGrab = false;
        yield return new WaitForSeconds(0.3f);
        canGrab = true;
    }
}
