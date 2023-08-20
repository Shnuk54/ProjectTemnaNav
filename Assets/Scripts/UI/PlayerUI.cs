using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image crosshair;
    [SerializeField] Image heartBeat;
    [SerializeField] Image endurance;
    [SerializeField] Image eyeEndurance;

    [Header("CrosshairImages")]
    [SerializeField] Sprite crosshairHand;
    [SerializeField] Sprite crosshairStandart;

    private HeartBeatSounds heart;
    private Breath breath;
    private IEnumerator ShowHeartBeat()
    {
        Vector2 startSize = heartBeat.rectTransform.localScale;
        while (true)
        {
            heartBeat.rectTransform.localScale = new Vector2(startSize.x + 0.2f, startSize.y + 0.2f);
            yield return new WaitForSeconds(heart.HeartBeatDelay/2f);
            heartBeat.rectTransform.localScale = new Vector2(startSize.x, startSize.y);
            yield return new WaitForSeconds(heart.HeartBeatDelay / 2f);
        }
    }
        
    private IEnumerator ShowBreath()
    {
        Vector2 startSize = endurance.rectTransform.localScale;
        while (true)
        {
            endurance.rectTransform.localScale = new Vector2(startSize.x + 0.2f, startSize.y + 0.2f);
            yield return new WaitForSeconds(breath.BreathDelay/2f);
            endurance.rectTransform.localScale = new Vector2(startSize.x, startSize.y);
            yield return new WaitForSeconds(breath.BreathDelay/2f);
        }
    }
    private void Start()
    {
        heart = FindObjectOfType<HeartBeatSounds>();
        breath = FindObjectOfType<Breath>();
        StartCoroutine("ShowBreath");
        StartCoroutine("ShowHeartBeat");

    }
    private void GrabCrosshair(bool grab)
    {
        if (grab)
        {
            crosshair.sprite = crosshairHand;
        }
        else
        {
            crosshair.sprite = crosshairStandart;
        }
    }
    private void FixedUpdate()
    {
        if (PlayerStateHandler.instance.IsHoldingItem)
        {
            GrabCrosshair(true);
            return;
        }
        GrabCrosshair(PlayerStateHandler.instance.IsSeeItem);
    }
}
