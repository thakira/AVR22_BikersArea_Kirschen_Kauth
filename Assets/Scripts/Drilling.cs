using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drilling : MonoBehaviour
{
    [SerializeField] private bool inTrigger;
    [SerializeField] private ScrAkkuschrauber schrauberScript;
    [SerializeField] private Animator AnimatorSchrauben;
    [SerializeField] private bool screwedIn;
    
    
     // 0 = Rahmen, 1 = Lenker, 2 = Vorderrad, 3 = Hinterrad, 4 = Zahnrad links, 5 = Zahnrad rechts, 6 = Pedale links, 7 = Pedale rechts
     [SerializeField, Tooltip("Rahmen:0, Lenker:1, Vorderrad:2, Hinterrad:3, Zahnrad links:4, Zahnrad rechts:5, Pedale links:6, Pedale rechts:7")]
     private int teilenummer; 
    



    // Update is called once per frame
    void Update()
    {
        if (schrauberScript.isActive && inTrigger)
        {
            StartCoroutine("DrillTimer");
            if(!screwedIn) { 
            AnimatorSchrauben.Play("Schraube");
            Debug.Log("Schraube jetzt drehen");
            }
            else
            {
                WerkstattSceneManager.instance.IsTightened(teilenummer);
                Debug.Log(teilenummer + " angeschraubt!");
                StopDrilling();
            }
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Schrauber"))
        {
            inTrigger = true;       
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Schrauber"))
        {
            StopDrilling();
        }
    }

    private void StopDrilling()
    {
        inTrigger = false;
        screwedIn = false;
        AnimatorSchrauben.Play("Idle");
        Debug.Log("Schrauben aufhören!");
    }
    IEnumerator DrillTimer()
    {
        yield return new WaitForSeconds(2);
        screwedIn = true;

    }

    public void setTeilenummer(int no)
    {
        this.teilenummer = no;
    }
}
