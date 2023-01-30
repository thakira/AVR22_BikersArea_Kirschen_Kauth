using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ScrAkkuschrauber : MonoBehaviour
{
    // [SerializeField] private GameObject druecker;
    // [SerializeField] private GameObject nuss;
    [SerializeField] private Animator activateDrill;
    [SerializeField] private Animator rotateNut;
    // [SerializeField] private AudioSource drilling;
    [SerializeField] private bool grabbed;
    public bool isActive;
    //[SerializeField] private Schrauben schrauben;

    void Update()
    {
        if (grabbed)
        {
            List<InputDevice> myDevice = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, myDevice);
            if (myDevice.Count == 1)
            {
                CheckController(myDevice[0]);
            }
        }
    }

    private void CheckController(InputDevice d)
    {
        bool primaryButtonDown;
        d.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonDown);
        if (primaryButtonDown)
        {
           //Debug.LogWarning("A gedrückt!");
            StartCoroutine("StartDrill");
        }
        else
        {
            //Debug.LogWarning("A nicht gedrückt!");
            StartCoroutine("StopDrill");
        }
    }


    public void ToggleGrabbed(bool pGrabbed)
    {
        this.grabbed = pGrabbed;
    }
    


    IEnumerator StartDrill()
    {
        //Debug.LogWarning("Knopf rein");
        //WerkstattSceneManager.instance.DrillActive(true);
        isActive = true;
        gameObject.GetComponent<AudioSource>().Play();
        //drilling.Play();
        activateDrill.Play("AkkuschrauberAn");
        rotateNut.Play("AnimBit");
        yield return null;
    }

    IEnumerator StopDrill()
    {
        isActive = false;
        //WerkstattSceneManager.instance.DrillActive(false);
        //Debug.LogWarning("Knopf raus");
        gameObject.GetComponent<AudioSource>().Stop();
       //drilling.Stop();
        activateDrill.Play("AkkuschrauberAus");
        rotateNut.Play("Idle");

        yield return null;
    }
}