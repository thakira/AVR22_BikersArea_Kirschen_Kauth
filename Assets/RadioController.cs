using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    bool isPlaying = false;

    private void OnTriggerEnter(Collider other)
    {
        if(isPlaying)
        {
            gameObject.transform.GetComponent<AudioSource>().Stop();
            isPlaying = false;
        }
        else
        {
            isPlaying = true;
            gameObject.transform.GetComponent<AudioSource>().Play();
        }
    }
}
