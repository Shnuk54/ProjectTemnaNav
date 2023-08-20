using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICameraPosition 
{
   public Transform PosTransform { get; } 
   public bool CanSeeTarget { get; }
   public float Distance { get; }

}
