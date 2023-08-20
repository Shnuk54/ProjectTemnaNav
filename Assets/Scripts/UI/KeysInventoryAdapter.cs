using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysInventoryAdapter : MonoBehaviour
{
    [SerializeField]private List<GameObject> uiItems;
    [SerializeField] GameObject prefab;

    private Inventory inventory;
    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }
    private void Start()
    {
      
        DrawItems(inventory.Keys);
    }
    private void OnEnable()
    {
        DrawItems(inventory.Keys);
    }
    private void DrawItems(List<StorableItem> items)
    {
        foreach (var item in uiItems)
        {
            Destroy(item);
        }

        foreach (var item in items)
        {
           GameObject obj = Instantiate(prefab,transform);
            uiItems.Add(obj);
            obj.GetComponent<InventoryItemElement>().SetupElement(item.itemName,item.icon,item.description);
        }
    }

}
