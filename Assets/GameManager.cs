using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private bool frameInGrab = false;

    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        } 
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
