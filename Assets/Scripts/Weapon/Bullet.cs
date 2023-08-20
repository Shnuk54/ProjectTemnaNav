using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour,IBullet
{   
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxXSpread;
    [SerializeField] private float _maxYSpread;
    [SerializeField] private Bullets bulletType;
    [SerializeField] private int _destroyDelay = 10;
    private Rigidbody _rb;
     
    void Awake() {
        _rb = GetComponent<Rigidbody>();
    }
    void Start() {
        BulletFly();
        Destroy(this.gameObject, _destroyDelay);
    }
    void OnTriggerEnter(Collider other) {
        if(other.GetComponent<IAlive>() == null){;return; }
        IAlive target = other.GetComponent<IAlive>();
        DealDamage(target);
        Destroy(this.gameObject);
    }
    public void DealDamage(IAlive target){
        target.TakeDamage(_damage);
    }
    public void BulletFly(){
        AddRandomForce();
    }
    private void AddRandomForce(){
        float spreadX = UnityEngine.Random.Range(-_maxXSpread,_maxXSpread);
        float spreadY = UnityEngine.Random.Range(-_maxYSpread,_maxYSpread);
        _rb.AddRelativeForce(new Vector3(spreadX,spreadY,this.transform.position.z)*_speed,ForceMode.Impulse);
    }
}
