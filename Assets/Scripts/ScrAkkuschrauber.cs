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
    [SerializeField] private AudioSource drilling;
    [SerializeField] private bool grabbed;

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
            Debug.LogWarning("A gedrückt!");
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
        Debug.LogWarning("Knopf rein");
        activateDrill.Play("AkkuschrauberAn");
        rotateNut.Play("AnimBit");
        drilling.Play();
        yield return null;
    }

    IEnumerator StopDrill()
    {
        Debug.LogWarning("Knopf raus");
        activateDrill.Play("AkkuschrauberAus");
        rotateNut.Play("Idle");
        drilling.Stop();
        yield return null;
    }
}