using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRaySelector 
{
   public void Check(Ray ray);
   public Transform GetSelection();
}
