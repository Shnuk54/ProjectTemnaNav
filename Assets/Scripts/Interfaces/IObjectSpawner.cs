using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectSpawner 
{
    public void SpawnObject(Transform transform,GameObject obj);
    public void SpawnObject(Transform transform);
}
