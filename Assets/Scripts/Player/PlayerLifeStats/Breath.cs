using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private List<AudioClip> _inhaleSounds;
    [SerializeField] private List<AudioClip> _exhaleSounds;

    [Header("Breath Settings")]
    [SerializeField] private float _delay;
    [SerializeField] private float _breathingSpeed;

    [Range(0,2f)]
    [SerializeField] private float _breathChangeSpeed;
    [Range(0, 60)]
    [SerializeField] private int _targetBreathSpeed;

    [Header("Endurance")]
    [SerializeField] private float _endurance;
    [SerializeField] private float _maxEndurance;
    [SerializeField] private float _enduranceLostSpeed;
    [SerializeField] private float _enduraceRestoreSpeed;
    
    private AudioSource _source;

    private void Start()
    {
        _delay = 60f / _breathingSpeed;
        _source = GetComponent<AudioSource>();
        _endurance = _maxEndurance;
        StartBreathing();
    }
    public float TargetBreathSpeed
    {
        get { return _targetBreathSpeed; }
        set
        {
            _targetBreathSpeed = Mathf.RoundToInt(value);
        }
    }
    public float BreathDelay
    {
        get { return _delay; }
    }
    public float BreathChangeSpeed
    {
        get { return _breathChangeSpeed; }
        set
        {
            _breathChangeSpeed = value;
        }
    }
    public float BreathingSpeed
    {
        get { return _breathingSpeed; }
        set
        {
            _breathingSpeed = value;
        }   
    }

    public float Endurance
    {
        get { return _endurance; }
        set { _endurance = value; }
    }
    public float EnduranceLostSpeed
    {
        get { return _enduranceLostSpeed; }
        set { _enduranceLostSpeed = value; }
    }

    public float EnduranceRestoreSpeed
    {
        get { return _enduraceRestoreSpeed; }
        set { _enduraceRestoreSpeed = value; }
    }
    public float MaxEndurance
    {
        get { return _maxEndurance; }
        set { _maxEndurance = value; }
    }

    private void FixedUpdate()
    {
        if (PlayerStateHandler.instance.PlayerState.isDead == false) {
            SmoothChangeBreathSpeed();
            LostEndurance();
            RestoreEndurance();
        } 
    }
    private void SmoothChangeBreathSpeed()
    {
        if (_endurance < _maxEndurance / 4)
        {
            _targetBreathSpeed = 45;
        }
        if (_endurance < _maxEndurance / 3 && _endurance > _maxEndurance/4)
        {
            _targetBreathSpeed = 40;
        }
        if (_endurance < _maxEndurance / 2 && _endurance > _maxEndurance/3)
        {
            _targetBreathSpeed = 30;
        }
        if (_endurance < _maxEndurance / 1.2f && _endurance  > _maxEndurance / 2)
        {
            _targetBreathSpeed = 25;
        }

        if (_breathingSpeed == _targetBreathSpeed) return;
        if (_breathingSpeed > _targetBreathSpeed || _breathingSpeed < _targetBreathSpeed)
        {
            _breathingSpeed = Mathf.Lerp(_breathingSpeed, _targetBreathSpeed, _breathChangeSpeed * Time.deltaTime);
            _source.volume = Mathf.Lerp(_source.volume, _targetBreathSpeed/60f, _breathChangeSpeed/2 * Time.deltaTime);
           
            var gate = _targetBreathSpeed - _breathingSpeed;
            gate = Mathf.Abs(gate);
            if (gate < 0.4f && _breathingSpeed != _targetBreathSpeed) _breathingSpeed = Mathf.Round(_breathingSpeed);
            _delay = 60f / _breathingSpeed;
        }

       

    }

    private void LostEndurance()
    {
        if (_endurance < 0) return;
        if (PlayerStateHandler.instance.PlayerState.isSprinting)
        {
            _endurance -= _enduranceLostSpeed * 2 * Time.deltaTime;

        }
        if (PlayerStateHandler.instance.PlayerState.isMoving)
        {
            _endurance -= _enduranceLostSpeed * Time.deltaTime;
        }

    }

    private void RestoreEndurance()
    {
        if (_endurance >= _maxEndurance)
        {
            _endurance = _maxEndurance;
            return;
        } 

        if(PlayerStateHandler.instance.PlayerState.isSprinting == false && PlayerStateHandler.instance.PlayerState.isMoving == false)
        {
            _endurance += _enduraceRestoreSpeed * Time.deltaTime;
        }   
    }
        private void StartBreathing()
    {
        StartCoroutine("Breathing");
    }
    private IEnumerator Breathing()
    {
        while (PlayerStateHandler.instance.PlayerState.isDead == false)
        {
            _source.clip = _inhaleSounds[Random.Range(0,_inhaleSounds.Count-1)];
            _source.Play();
            yield return new WaitForSeconds(_delay/2);
            _source.clip = _exhaleSounds[Random.Range(0, _exhaleSounds.Count - 1)];
            _source.Play();
            yield return new WaitForSeconds(_delay / 2);
        }
        
    }
}
