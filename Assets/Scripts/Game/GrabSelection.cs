using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSelection :MonoBehaviour,ISelectionResponse
{
    [SerializeField] private IDragable _draggableObject;
    [SerializeField] private IItem _itemObject;
    [SerializeField] private ILockable _lock;
    private Inventory _inventory;
    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
    }
    public void OnSelect(Transform selection)
    {
        var selectionObj = selection;
       
        if (selectionObj.GetComponent<IItem>()!= null && PlayerStateHandler.instance.PlayerState.isAiming)
        {
          
            _inventory.AddItem(selectionObj.GetComponent<IItem>());
        }
        if (selectionObj.GetComponent<IDragable>() != null)
        {
            _draggableObject = selectionObj.GetComponent<IDragable>();
            _draggableObject.Grab();
        }
        if (selectionObj.GetComponent<ILockable>() != null&&PlayerStateHandler.instance.PlayerState.isAiming)
        {
            _lock = selectionObj.GetComponent<ILockable>();
            _lock.Unlock(_inventory.GetKey(_lock.KeyName));
        }


    }
    public void OnDeselect(Transform selection)
    {
        var selectionObj = selection?.GetComponent<IDragable>();
        _draggableObject = selectionObj;

    }

}
