using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponitem : MonoBehaviour,IItem
{
   public bool Selected{get;set;}
    public StorableItem storableItem { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void Update() {
        if(Selected && Input.GetButtonDown("Submit")){
            Use();
        }
    }
   public void Use(){
    FindObjectOfType<WeaponInventory>().AddWeapon(this.gameObject);
    this.transform.SetParent(FindObjectOfType<WeaponInventory>().GetComponent<Transform>());
   }

    public void Drop()
    {
        throw new System.NotImplementedException();
    }

    public void Pickup()
    {
        throw new System.NotImplementedException();
    }

    public StorableItem GetItem()
    {
        throw new System.NotImplementedException();
    }
}
