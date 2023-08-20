using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPath : MonoBehaviour
{
    [SerializeField] private List<Vector3> _path = new List<Vector3>();
    [SerializeField] private float _delay = 0.1f;
    private Transform _transform;
    private bool _pathTracking = false;

    public bool PathTracking
    {
        get { return _pathTracking; }
        set { _pathTracking = value; }
    }
    public List<Vector3> Path
    {
        get { return _path; }
    }
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    public void SaveLastPath(bool save)
    {
        if(save == false)
        {
            _pathTracking = false;
            return;
        }
        else
        {
            _pathTracking = true;
            StartCoroutine("SavePath");
        }
        
    }
    public void ResetPath()
    {
        _path = new List<Vector3>();
    }
    private IEnumerator SavePath()
    {
      
        while (_pathTracking)
        {
            _path.Add(_transform.position);
            if (_path.Count > 20) _path.RemoveRange(0, 10);
            yield return new WaitForSeconds(_delay);
        }
    }
}
