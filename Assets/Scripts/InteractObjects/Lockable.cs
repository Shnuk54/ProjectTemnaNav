using System;
using UnityEngine;

public class Lockable : MonoBehaviour, ILockable
{
    [field: SerializeField] public bool IsLocked {  get; private set; } = true;

    [field: SerializeField] public string KeyName {  get;  private set; } = "Key";

    public void Unlock(string keyName)
    {
       if (IsLocked && keyName == KeyName)
        {
            IsLocked = false;
            this.enabled = false;
        }
    }
}

