
using UnityEngine;
using UnityEngine.UI;





[System.Serializable]public class StorableItem
{ 
    public string itemName;
    public string description;
    public float weight;
    public Texture icon;
    public ItemTypes itemType;
}
public enum ItemTypes
{
    Food,Key,Healing,Bullet,Weapon
}