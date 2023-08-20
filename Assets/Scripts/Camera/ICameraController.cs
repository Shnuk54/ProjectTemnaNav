using System.Collections;
using UnityEngine;
using System.Collections.Generic;
namespace Assets.Scripts
{
    public interface ICameraController 
    {
        public void FollowTarget(Transform target);
        public void ChangePosition(Transform newPos);
        public void ChangePosition(List<Vector3> path);
        public bool IsMoving { get; }
    }
}