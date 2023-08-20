using System.Collections;
using UnityEngine;


    public class CamPosition : MonoBehaviour, ICameraPosition
    {

        [SerializeField] private float _distanceToTarget;
        [SerializeField] private bool _canSeeTarget;
        [SerializeField] private float _maxFollowDistance;
        private Transform _target;
        private Transform _transform;
        private RaycastViewBlockingCheck _viewCheck;
        public Transform PosTransform { get { return _transform; } }
        public bool CanSeeTarget {
            get {
                if (_distanceToTarget <= _maxFollowDistance) return _viewCheck.IsVisible;
                else return false;  
                 }
        }
        public float Distance { get { return Vector3.Distance(_transform.position,_target.position); } }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _target = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
            _viewCheck = GetComponent<RaycastViewBlockingCheck>();
        }

    private void Update()
    {
        VisibleCheck();
    }
    private void OnDrawGizmos()
    {
        _transform = GetComponent<Transform>();
        Gizmos.color = new Color(0, 1, 0, 0.05f);
        Gizmos.DrawSphere(_transform.position, _maxFollowDistance);
    }
    private void VisibleCheck()
    {
        _distanceToTarget = Vector3.Distance(_transform.position, _target.position);
        if (_distanceToTarget > _maxFollowDistance) _canSeeTarget = false; 
        if(_distanceToTarget <= _maxFollowDistance) _canSeeTarget = _viewCheck.IsVisible;


    }
}
