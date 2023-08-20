using System.Collections;
using UnityEngine;


    public class ScareZone : MonoBehaviour
    {
    [SerializeField] float Scare;
    [SerializeField] float ScareSpeed;

    private bool _alreadyIn = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && _alreadyIn == false)
        {
            ScarryMoment scare = new ScarryMoment();
            scare.scareSpeed = ScareSpeed;
            scare.stress = Scare;
            Events.instance.OnPlayerScared(scare);
            _alreadyIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && _alreadyIn == true) _alreadyIn = false;
    }
}
