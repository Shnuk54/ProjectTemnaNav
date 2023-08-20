using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public interface  IItem 
{
   public void Use();
   public bool Selected{get;set;}
   public void Drop();
   public void Pickup();

    public StorableItem GetItem();
      
}
