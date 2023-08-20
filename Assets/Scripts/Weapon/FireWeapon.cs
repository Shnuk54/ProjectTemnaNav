using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour,IWeapon
{
    [SerializeField]private float _shootRate;
    [SerializeField] Bullets bulletsType;
    [SerializeField] private bool _canShoot; 
    [SerializeField] private int _magazineCapacity;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _releasedAmmo;
    [SerializeField] Transform _burrel;
    [SerializeField] string _shootSound;
    [SerializeField] string _reloadSound;
    private IObjectSpawner _spawner;
    private WaitForSeconds _seconds;
    private WaitForSeconds _secondsReloadTime;
    void Start() {
        _seconds = new WaitForSeconds(60f/_shootRate);
        _secondsReloadTime = new WaitForSeconds(_reloadTime);
        _spawner = GetComponent<IObjectSpawner>();
    }
    public void Reload(){

    }
    public void Shoot(){
        if(_canShoot == false) return;
        if(_releasedAmmo >= _magazineCapacity) StartCoroutine("ReloadDelay");
        if(_canShoot){
           ParticleController.instance.PlayParticle("Shotgun",_burrel);
            _spawner.SpawnObject(this.transform);
            AudioManager.singleton.Play(_shootSound);
            _releasedAmmo++;
            StartCoroutine("ShootRate");
        }
    }
    public void ChangePos(Transform newPos){
    transform.position = newPos.position;
    transform.rotation = newPos.rotation;}
    
    private IEnumerator ShootRate(){
        _canShoot = false;
        yield return _seconds;
        _canShoot = true;
    }
    private IEnumerator ReloadDelay(){
        _canShoot = false;
        _releasedAmmo = 0;
        yield return _secondsReloadTime;
        _canShoot = true;
    }
}
