
using UnityEngine;
using System;


public class ParticleController : MonoBehaviour,IParticleController
{
  [SerializeField] private Particle[] _particls;
  public static ParticleController instance;
   private IObjectSpawner _spawner;
   void Awake() {
      if(!instance){
            instance = this;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(gameObject);
        }
   }
  void Start() {
    _spawner = GetComponent<IObjectSpawner>();
  }
      public void PlayParticle(string name,Transform transform)
    {
        Particle p = Array.Find(_particls, particle => particle.name == name);
        _spawner.SpawnObject(transform, p.particle);
        p.particle.GetComponent<ParticleSystem>().Play();
    }

}
