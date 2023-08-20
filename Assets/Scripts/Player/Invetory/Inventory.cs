using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] List<StorableItem> items;
    [Header("Keys")]
    [SerializeField] List<StorableItem> keys;

    [Header("UI")]
    [SerializeField] GameObject keysUI;
    private bool inInvetory = false;

    private void Start()
    {
        keysUI.SetActive(false);
    }
    private void Update()
    {
        if(InputHandler.instance.inventory)
        {
            if (inInvetory == true)
            {
                inInvetory = false;
                PlayerStateHandler.instance.InInventory = false;
                keysUI.SetActive(false);
            }
            else
            {
                PlayerStateHandler.instance.InInventory = true;
                inInvetory = true;
                keysUI.SetActive(true);
            }
           
        }
        
    }


    public List<StorableItem> Items
    {
        get
        {
            return items;
        }
    }
    public List<StorableItem> Keys
    {
        get
        {
            return keys;
        }
    }
    public void AddItem(IItem item)
    {
        if(item.GetItem().itemType == ItemTypes.Key)
        {
            keys.Add(item.GetItem());
        }
        item.Pickup();
    }

    public string GetKey(string keyName)
    {
        foreach(StorableItem key in keys)
        {
            if(keyName == key.itemName)
            {
                return key.itemName;
            }
        }
        return null;
    }

}
