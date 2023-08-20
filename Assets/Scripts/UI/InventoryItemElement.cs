using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemElement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] RawImage image;
    [SerializeField] TextMeshProUGUI info;

    public void SetupElement(string ItemName,Texture Image,string Info)
    {
        itemName.text = ItemName;
        image.texture = Image;
        info.text = Info;
    }
}
