using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    [SerializeField] private AnimationCurve _blinkDuration;
    [SerializeField] private bool _eyesClosed = false;
    [SerializeField] public bool startBlinking = false;
    [SerializeField] private Image _image;
    [SerializeField] private float _eyeEndurance;
    [SerializeField][Range(0.1f, 5)] private float _eyeTiredSpeed;
    [SerializeField] private float _maxEyeEndurance;
    private float _time;
    private float _pastTime;

    private void Start()
    {
        _time = _blinkDuration.keys[_blinkDuration.keys.Length - 1].time;
    }

    public float EyeEndurance
    {
        get { return _eyeEndurance; }
    }
    public float MaxEyeEndurace
    {
        get { return _maxEyeEndurance; }
        set { _maxEyeEndurance = value; }
    }

    public float EyeTiredSpeed
    {
        get { return _eyeTiredSpeed; }
        set { _eyeTiredSpeed = value; }
    }
    private void OnEnable()
    {
        Events.onPlayerStartBlink += Blink;
    }
    private void OnDisable()
    {
        Events.onPlayerStartBlink -= Blink;
    }
    private void Blink()
    {
        StartCoroutine("MakeBlink");
    }
    private void LostEyeEndurance()
    {
        if (_eyeEndurance <= 0 && startBlinking == false)
        {
            Events.instance.OnPlayerStartBlink();
            return;

        }
        _eyeEndurance -= _eyeTiredSpeed * Time.deltaTime;

    }

    private void RestoreEyeEndurance()
    {
        _eyeEndurance = _maxEyeEndurance;
    }
    private void FixedUpdate()
    {
        LostEyeEndurance();
    }
    private IEnumerator MakeBlink()
    {
        startBlinking = true;
        while (_pastTime < _time)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _blinkDuration.Evaluate(_pastTime));
            _pastTime += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
           
            if (_image.color.a >= 0.8f && _eyesClosed == false)
            {
                _eyesClosed = true;
                RestoreEyeEndurance();
                Events.instance.OnPlayerBlink();
            } 
        }
        _pastTime = 0;
        _eyesClosed = false;
        startBlinking = false;
    }
}
