using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour,IObjectSpawner
{
   [SerializeField] private GameObject  _bullet;
   [SerializeField] private Transform _barrel;
   public void SpawnObject(Transform transform,GameObject obj)=>Instantiate(obj,_barrel.position,_barrel.rotation);
   public void SpawnObject(Transform transform)=>Instantiate(_bullet,_barrel.position,_barrel.rotation);
   
}
