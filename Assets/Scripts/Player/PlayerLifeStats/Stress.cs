using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress : MonoBehaviour
{
    [Header("Stress")]
    [SerializeField] private float _stress;
    [SerializeField] private float _calmingSpeed;
    [SerializeField] private float _changeSpeed;
    [SerializeField] private float _minimumStress = 0;
    [SerializeField] private float _targetStress;
    [SerializeField] private StressStatus _status;

    
    [Header("ScareMoments")]
    [SerializeField] List<ScarryMoment> _scaresList;
    [SerializeField] private bool _stressedUp;
    [SerializeField] private bool _alreadyStressed;
    [SerializeField] private bool _calmedDown;
    [SerializeField] private float _nextScareDelay;
    [SerializeField] private float _timeBeforeCalming;
    [SerializeField] private float _pastTime;

    private ScarryMoment _scare;

    public float TargetStress{
        get { return _targetStress; }
        set { _targetStress = value; }
    }
    public float StressValue
    {
        get { return _stress; }
        
    }
    public float StressChangeSpeed
    {
        get { return _changeSpeed; }
    }
    public StressStatus StressStatus
    {
        get { return _status; }
    }

    public void Scare(ScarryMoment scare)
    {
     _stressedUp = true;
     _scaresList.Add(scare);
     StartCoroutine("ScareUp");
    }
    private void HandleScare(ScarryMoment scare)
    {

       if(scare.stress > 0)
        {
            _stressedUp = true;
            _changeSpeed = scare.scareSpeed;
        }
       

        if (_targetStress + scare.stress > 100) {
            _targetStress = 100;
          
            if (_alreadyStressed)
            {
                _changeSpeed = scare.scareSpeed/3f;
             }
     
            return;
        } 

        if (_targetStress + scare.stress < _minimumStress)
        {
            _targetStress = _minimumStress;
            _changeSpeed = _calmingSpeed;
            return;
        } 

        _targetStress += scare.stress;
       
    }

    private IEnumerator ScareUp()
    {
        if (_stressedUp == false) yield return null;

        yield return new WaitForSeconds(2f);

        foreach(ScarryMoment scare in _scaresList)
        {
            _scare.scareSpeed += scare.scareSpeed;
            _scare.stress += scare.stress;
        }
        HandleScare(_scare);

        _scare.scareSpeed = 0;
        _scare.stress = 0;
        _scaresList.Clear();
    }

    private void FixedUpdate()
    {
        SmoothChangeStress(_changeSpeed,_minimumStress);
        
    }
    private void Update()
    {
        StressTimer();
    }
    private void StressTimer()
    {
        if (_stressedUp)
        {
            _calmedDown = false;
            _alreadyStressed = true;
            _pastTime += Time.deltaTime;
           
            if (_pastTime >= _nextScareDelay)
            {
                _alreadyStressed = false;
            } 

            if (_pastTime >= _timeBeforeCalming && _alreadyStressed == false)
            {
                _pastTime = 0;
                _alreadyStressed = false;
                _stressedUp = false;
                _calmedDown = true;
            }
        }
    }

    private void SmoothChangeStress(float changeSpeed,float minimumStress)
    {
        if (_calmedDown)
        {
            _targetStress = Mathf.Lerp(_targetStress, minimumStress, _calmingSpeed * Time.deltaTime);
            var gate = _targetStress - minimumStress;
            gate = Mathf.Abs(gate);
            if (gate < 0.4f && minimumStress != _targetStress) _targetStress = Mathf.Round(_targetStress);
        }
       

        if (_stress == _targetStress) {
            return;
        }
        
        if (_stress > _targetStress || _stress < _targetStress)
        {
            _stress = Mathf.Lerp(_stress, _targetStress, changeSpeed * Time.deltaTime);
            var gate = _targetStress - _stress;
            gate = Mathf.Abs(gate);
            if (gate < 0.4f && _stress != _targetStress) _stress = Mathf.Round(_stress);

        }

        if (_stress <= 10) _status = StressStatus.Ñalmn;
        if(_stress > 10 && _stress < 25) _status = StressStatus.LowStress;
        if (_stress > 25 && _stress < 50) _status = StressStatus.MediumStress;
        if (_stress > 50 && _stress < 80) _status = StressStatus.HighStress;
        if (_stress > 80 && _stress <= 100) _status = StressStatus.Scared;

        
    }
}

    

public  enum StressStatus { Ñalmn, LowStress, MediumStress, HighStress, Scared };


[System.Serializable]
public struct ScarryMoment
{
    public float stress;
    public float scareSpeed;
}