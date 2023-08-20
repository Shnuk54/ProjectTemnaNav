using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastViewBlockingCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _layers;
    [SerializeField] bool _isVisible;
    [SerializeField] Transform _target;
    private void Start()
    {
        _target = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
    }
    public bool IsVisible
    {
        get { return DrawRay(); }

    }
    private bool DrawRay()
    {
        RaycastHit hit;
        Vector3 direction = _target.transform.position - this.transform.position;
        var ray = Physics.Raycast(this.transform.position, direction, out hit, _layers);
        if (hit.collider == null) return _isVisible = true;
        Debug.DrawRay(this.transform.position, direction, Color.blue);
        if(hit.collider.tag == "Wall" || hit.collider.tag == "Ground")
        {
            Debug.DrawRay(this.transform.position, direction, Color.red);
            return _isVisible = false;
        }
        return _isVisible = true;
    }
    private void OnDrawGizmos()
    {
        _target = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        DrawRay();
    }
    private void Update()
    {
        DrawRay();
    }
}
