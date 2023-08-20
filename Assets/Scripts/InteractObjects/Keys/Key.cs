using System;
using Unity.VisualScripting;
using UnityEngine;

public class Key :MonoBehaviour , IItem
{
    [field: SerializeField] public string KeyName { get; private set; } = "Key";
    public bool Selected { get; set; }
    [SerializeField] private StorableItem storableItem;

    public void Drop()
    {
        throw new NotImplementedException();
    }

    public void Pickup()
    {
        Debug.Log(storableItem.itemType);
        Destroy(this.gameObject);
    }

    public void Use(ILockable keyLock)
    {
        storableItem.itemName = KeyName;
        keyLock.Unlock(KeyName);
    }

    public void Use()
    {
        throw new NotImplementedException();
    }

    public StorableItem GetItem()
    {
        return storableItem;
    }
}

