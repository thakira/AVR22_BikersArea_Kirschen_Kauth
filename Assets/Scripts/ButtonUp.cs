using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUp : MonoBehaviour
{
    [SerializeField] private Animator UpButton;

    [SerializeField] private GameObject grappler;
    private bool moveGrappler;
    private float endPosition = 0.65f;
    private float speed = 0.2f;


    // Update is called once per frame
    void Update()
    {
        if (moveGrappler)
            
        {
            // Debug.LogWarning("Grappler starts moving");
            // Debug.LogWarning("localpos.z: " + grappler.transform.localPosition.z + (grappler.transform.localPosition.z < endPosition).ToString());
            // Debug.LogWarning("localpos.z: " + grappler.transform.localPosition.z);
            // Debug.LogWarning("Grappler is moving");
            if (grappler.transform.localPosition.z >= endPosition)
            {
                moveGrappler = false;
            }
            grappler.transform.position += Vector3.up * speed * Time.deltaTime;
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.LogWarning(other.name + " berührt Knopf");
        if(other.CompareTag("hand")) {
            // Debug.LogWarning("Hand berührt Knopf");
            // UpButton.Play("UpButtonPressed");
            moveGrappler = true;
            StartCoroutine("MoveButton");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("hand"))
        {
            // Debug.LogWarning("Hand ist raus");
            moveGrappler = false;
            StartCoroutine("StopButton");
            // UpButton.Play("UpButtonReleased");
        }

    }

    IEnumerator MoveButton()
    {
        // Debug.LogWarning("Knopf rein");
        UpButton.Play("UpButtonPressed");
        yield return null;
    }
    
    IEnumerator StopButton()
    {
        // Debug.LogWarning("Knopf raus");
        UpButton.Play("UpButtonReleased");
        yield return null;
    }
}