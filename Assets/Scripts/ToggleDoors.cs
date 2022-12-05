using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToggleDoors : MonoBehaviour
{
    [SerializeField] private Animator DoorA = null;
    [SerializeField] private Animator DoorB = null;
   



    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Trigger ausgelöst durch: " + other.name);
        if (other.CompareTag("hand"))
        {
            StartCoroutine("openDoor");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.LogWarning("Trigger verlassen durch: " + other.name);
        if (other.CompareTag("hand"))
        {
            StartCoroutine("closeDoor");
        }
    }

    IEnumerator openDoor()
    {
        Debug.LogWarning("Tür öffnen");
        DoorA.Play("openDoorA", 0, 0.0f);
        DoorB.Play("openDoorB", 0, 0.0f);
        yield return null;
    }
    
    IEnumerator closeDoor()
    {
        Debug.LogWarning("Tür schliessen");
        DoorA.Play("closeDoorA", 0, 0.0f);
        DoorB.Play("closeDoorB", 0, 0.0f);
        yield return null;
    }
    
    
    
    
  
    
}