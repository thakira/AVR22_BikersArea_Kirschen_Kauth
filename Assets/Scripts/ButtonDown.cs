using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ButtonDown : MonoBehaviour
{
    [SerializeField] private Animator DownButton;

    [SerializeField] private GameObject grappler;
    private bool moveGrappler;
    private float endPosition = 0.25f;
    private float speed = 0.2f;


    // Update is called once per frame
    void Update()
    {
        if (moveGrappler)
            
        {
            Debug.LogWarning("Down Grappler starts moving");
            // Debug.LogWarning("localpos.z: " + grappler.transform.localPosition.z + (grappler.transform.localPosition.z < endPosition).ToString());
            //  Debug.LogWarning("Down localpos.z: " + grappler.transform.localPosition.z);
            // Debug.LogWarning("Down Grappler is moving");
            if (grappler.transform.localPosition.z <= endPosition)
            {
                moveGrappler = false;
            }
            grappler.transform.position += Vector3.down * speed * Time.deltaTime;
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning(other.name + " berührt Knopf Down ");
        if(other.CompareTag("hand")) {
            // Debug.LogWarning("Hand berührt Knopf Down ");
            // UpButton.Play("UpButtonPressed");
            moveGrappler = true;
            StartCoroutine("MoveButton");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("hand"))
        {
            Debug.LogWarning("Hand ist raus Down ");
            moveGrappler = false;
            StartCoroutine("StopButton");
            // UpButton.Play("UpButtonReleased");
        }

    }

    IEnumerator MoveButton()
    {
        Debug.LogWarning("Knopf rein Down ");
        DownButton.Play("UpButtonPressed");
        yield return null;
    }
    
    IEnumerator StopButton()
    {
        Debug.LogWarning("Knopf raus Down");
        DownButton.Play("UpButtonReleased");
        yield return null;
    }
}