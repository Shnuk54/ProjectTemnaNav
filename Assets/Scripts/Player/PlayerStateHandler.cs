using System.Collections;
using UnityEngine;



    public class PlayerStateHandler : MonoBehaviour
    {
    [SerializeField] PlayerState _playerState;
    public static PlayerStateHandler instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public PlayerState PlayerState
    {
        get { return _playerState; }
    }
    public bool IsSeeItem
    {
        get { return _playerState.isSeeItem; }
        set {  _playerState.isSeeItem = value; }
    }
    public bool IsHoldingItem
    {
        get { return _playerState.isHoldingItem; }
        set { _playerState.isHoldingItem = value; }
    }
    public bool InInventory
    {
        get { return _playerState.inInventory; }
        set { _playerState.inInventory = value; }
    }
    private void Update()
    {
        UpdateState();
    }
    private void UpdateState()
    {
        _playerState.isAiming = InputHandler.instance.aiming;

        if(InputHandler.instance.verInput != 0 || InputHandler.instance.horInput != 0)
        {
            _playerState.isMoving = true;
        }
        else
        {
            _playerState.isMoving = false;
        }
        _playerState.isSprinting = InputHandler.instance.sprinting;
       
    }
}
