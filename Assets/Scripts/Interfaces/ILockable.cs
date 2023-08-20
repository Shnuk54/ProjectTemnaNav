using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface ILockable
{

 
    public bool IsLocked{get;}
    public string KeyName {get;}
    public void Unlock(string keyName);
    

}

