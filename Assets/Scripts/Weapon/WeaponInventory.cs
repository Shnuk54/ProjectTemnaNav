using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weapons; 
    [SerializeField] private string _currentWeaponName;
    int id = 0;
    
    void Start() {
        Events.instance.OnPlayerChangeWeapon(_weapons[0].GetComponentInChildren<IWeapon>());
    }
    void Update() {
        if( Input.GetAxis("Mouse ScrollWheel") > 0){
            ChangeWeapon();
        }
    }
    private void ChangeWeapon(){
        _currentWeaponName = _weapons[id].name;
        id++;
        if(id > _weapons.Count - 1)id = 0;
        Events.instance.OnPlayerChangeWeapon(_weapons[id].GetComponentInChildren<IWeapon>());
    }
    public void AddWeapon(GameObject weapon){
        _weapons.Add(weapon);
        id = _weapons.Count - 1;
        Events.instance.OnPlayerChangeWeapon(_weapons[id].GetComponentInChildren<IWeapon>());
        
    }
    public List<GameObject> Weapons{
        get{
            return _weapons;
        }
        set{
            _weapons = value;
        }
    }
}
