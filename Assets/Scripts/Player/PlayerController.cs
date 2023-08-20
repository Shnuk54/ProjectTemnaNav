using UnityEditor;
using UnityEngine;


    public class PlayerController : MonoBehaviour
    {
    
    private PlayerMovement _movement;


    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
       
        _movement.Move();
        _movement.HeadRotation();
    }
    private void LateUpdate()
    {
        _movement.BodyRotatio();
    }
}
