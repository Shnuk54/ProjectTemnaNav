using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerLifeStatsController : MonoBehaviour
{
    [Header("Eyes")]
    [SerializeField] private float _eyeEndurance;
    [SerializeField] private float _eyeTiredSpeed;
    [SerializeField] private float _maxEyeEndurance;
    private Blinking _eyeHandler;

    [Header("Health")]
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;

    [Header("Endurance")]
    [SerializeField] private float _endurance;
    [SerializeField] private float _maxEndurance;
    [SerializeField] private float _enduranceLostSpeed;
    [SerializeField] private float _enduranceRestoreSpeed;
    private Breath _breathHandler;

    [Header("Stress")]
    [SerializeField] private float _stress;
    [SerializeField] private float _maxStress;
    [SerializeField] private float _targetStress;
    [SerializeField] private float _stressChangeSpeed;
    private Stress _stressHandler;

    [Header("Heart")]
    [SerializeField] private float _heartBeat;
    [SerializeField] private float _heartBeatChangeSpeed;
    [SerializeField] private float _targetHeartBeat;
    private HeartBeatSounds _heartHandler;

    [SerializeField] Text _Stress;
    [SerializeField] Text _StressStatus;
    [SerializeField] Text _Pulse;
    [SerializeField] Text _Endurance;
    [SerializeField] Text _EyeEndurance;
    [SerializeField] bool showDebug = false;
    private void Start()
    {
        _eyeHandler = FindObjectOfType<Blinking>().GetComponent<Blinking>();    
        _heartHandler = FindObjectOfType<HeartBeatSounds>().GetComponent<HeartBeatSounds>();
        _stressHandler = FindObjectOfType<Stress>().GetComponent<Stress>();
        _breathHandler = FindObjectOfType<Breath>().GetComponent<Breath>();
    }


    private void OnEnable()
    {
        Events.onPlayerScared += Scare;
    }
    private void OnDisable()
    {
        Events.onPlayerScared -= Scare;
    }
    private void HandleLifeStats()
    {
        _targetHeartBeat = _heartHandler.TargetPulse;
        _heartBeat = _heartHandler.Pulse;
        _heartBeatChangeSpeed = _heartHandler.PulseChangeSpeed;

        _targetStress = _stressHandler.TargetStress;
        _stress = _stressHandler.StressValue;
        _stressChangeSpeed = _stressHandler.StressChangeSpeed;

        _eyeTiredSpeed = _eyeHandler.EyeTiredSpeed;
        _eyeEndurance = _eyeHandler.EyeEndurance;
        _maxEyeEndurance = _eyeHandler.MaxEyeEndurace;

        _endurance = _breathHandler.Endurance;
        _enduranceLostSpeed = _breathHandler.EnduranceLostSpeed;
        _enduranceRestoreSpeed = _breathHandler.EnduranceRestoreSpeed;
        _maxEndurance = _breathHandler.MaxEndurance;
        if (showDebug)
        {
            _Stress.text = "Stress:" + _stress;
            _StressStatus.text = "Stress Status:" + _stressHandler.StressStatus;
            _Pulse.text = "Pulse:" + _heartBeat;
            _EyeEndurance.text = "Eye Endurance:" + _eyeEndurance;
            _Endurance.text = "Endurance:" + _endurance;
        }
      
    }
    private void FixedUpdate()
    {
        HandleLifeStats();
        ChangeLifeStats();
    }

    private void Scare(ScarryMoment scare)
    {
        _stressHandler.Scare(scare);
    }


    [ContextMenu("ChangeLifeStats")]
    private void ChangeLifeStats()
    {
        if(_stressHandler.StressStatus == StressStatus.Ñalmn)
        {
            _heartHandler.TargetPulse = Random.Range(45, 65);
            _eyeHandler.EyeTiredSpeed = 0.1f;
            
        }
        if(_stressHandler.StressStatus == StressStatus.LowStress)
        {
            _heartHandler.TargetPulse = Random.Range(65, 90);
            _eyeHandler.EyeTiredSpeed = 0.3f;
        }
        if(_stressHandler.StressStatus == StressStatus.MediumStress)
        {
            _heartHandler.TargetPulse = Random.Range(90, 125);
            _eyeHandler.EyeTiredSpeed = 0.6f;
        }
        if (_stressHandler.StressStatus == StressStatus.HighStress)
        {
            _heartHandler.TargetPulse = Random.Range(125, 150);
            _eyeHandler.EyeTiredSpeed = 0.9f;
        }
        if (_stressHandler.StressStatus == StressStatus.Scared)
        {
            _heartHandler.TargetPulse = Random.Range(150, 200);
            _eyeHandler.EyeTiredSpeed = 1.5f;
        }
    }
}
