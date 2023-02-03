using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrAkkuschrauber : MonoBehaviour
{
    public HandData rightHandPose;
    // [SerializeField] private GameObject druecker;
    // [SerializeField] private GameObject nuss;
    [SerializeField] private Animator activateDrill;
    [SerializeField] private Animator rotateNut;
    [SerializeField] private AudioSource drilling;
    [SerializeField] private bool grabbed;
    public bool isActive;
    [SerializeField] private Animator handAnimator;
    [SerializeField] private GameObject rightHand;
    //[SerializeField] private Schrauben schrauben;

    private void Awake()
    {
        rightHand = GameObject.FindGameObjectWithTag("HandAnimator");
        handAnimator = rightHand.GetComponent<Animator>();
    }

    private void Start()
    {
        // XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
    //     // grabInteractable.selectEntered.AddListener(SetupPose);
        rightHandPose.gameObject.SetActive(false);
    }

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
            handAnimator.enabled = false;
            //rightHandPose.gameObject.SetActive(true);
        }
        else
        {
            handAnimator.enabled = true;
            //rightHandPose.gameObject.SetActive(false);
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
            if(!drilling.isPlaying) 
            {
            drilling.Play();
            }
        }
        else
        {
            //Debug.LogWarning("A nicht gedrückt!");
            StartCoroutine("StopDrill");
            drilling.Stop();
        }
    }


    public void ToggleGrabbed(bool pGrabbed)
    {
        this.grabbed = pGrabbed;
    }
    
    // public void SetupPose(BaseInteractionEventArgs arg)
    // {
    //     if(grabbed) {
    //     handAnimator.enabled = false;
    //     rightHandPose.gameObject.SetActive(true);
    //     }
    //     else
    //     {
    //         handAnimator.enabled = true;
    //         rightHandPose.gameObject.SetActive(false);
    //     }
    // }
    


    IEnumerator StartDrill()
    {
        //Debug.LogWarning("Knopf rein");
        isActive = true;
        activateDrill.Play("AkkuschrauberAn");
        rotateNut.Play("AnimBit");
        yield return null;
    }

    IEnumerator StopDrill()
    {
        isActive = false;
        //WerkstattSceneManager.instance.DrillActive(false);
        //Debug.LogWarning("Knopf raus");
        activateDrill.Play("AkkuschrauberAus");
        rotateNut.Play("Idle");

        yield return null;
    }
}