using System;



public interface IKey
{

 
  public string KeyName { get;}
  public StorableItem storableItem { get; set; }
    public void Pickup();

}

