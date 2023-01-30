using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class WerkstattSceneManager : MonoBehaviour
{
    public static WerkstattSceneManager instance;

    // WerkstattSceneManager.instance.IsTightened(1);

    [SerializeField, Tooltip("Greifarm Socket, der �berpr�ft werden soll.")]
    private XRSocketInteractor grabSocketInteractor;

    [SerializeField, Tooltip("Reihenfolge beachten! -yellow-blue-red-pink-black-grey-green-")] 
    private Sprite[] bikeFrameImages;
    
    [SerializeField] private Image currentTaskImage;
    [SerializeField] private Image[] taskThumbnails;
    [SerializeField] private GameObject[] taskIcons;
 
    private bool[] mountedItems = { false, false, false, false, false, false, false, false };
    private bool[] tightenedItems = { true, false, false, false, true, true, false, false };

    private int[] tasks;

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

        GetRandomTasks(4);
        SetTaskThumbnails();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleFrameInGrab(bool isInGrab)
    {
        if (isInGrab)
        {
            CheckCorrectFrame("green");
        }
    }

    private void CheckCorrectFrame(string color)
    {
        Debug.Log("Aktuelle Rahmenfarbe: " + color);

        IXRSelectInteractable objName = grabSocketInteractor.GetOldestInteractableSelected();

        GameObject currentObject = objName.transform.gameObject;
        MeshRenderer myRend = currentObject.GetComponent<MeshRenderer>();

        Debug.Log(objName.transform.name + " in socket of " + transform.name);
        Debug.Log(currentObject.transform.name + " hat die Farbe " + myRend.materials[0].name);
    }

    private void GetRandomTasks(int i)
    {
        tasks = new int[] { 2, 4, 3, 6 };
    }

    private void SetTaskThumbnails()
    {
        currentTaskImage.sprite = bikeFrameImages[tasks[0]];
        Debug.Log("CurrentColor: " + currentTaskImage.sprite.name);

        for (int i = 0; i<taskThumbnails.Length; i++)
        {
            taskThumbnails[i].sprite = bikeFrameImages[tasks[i]+1];
        }
    }

    public void SetMountedIcon(int i)
    {
        mountedItems[i] = true;

        taskIcons[i].transform.Find("IconToDo").gameObject.SetActive(false);
        taskIcons[i].transform.Find("IconDone").gameObject.SetActive(true);

        if (i != 0)
        {
            taskIcons[i].transform.parent.Find("IconMounted").gameObject.SetActive(true);
            tightenedItems[i] = true;
        }

        if (i == 4)
        {

        }
    }

    public void SetUnmountedIcon(int i)
    {
        mountedItems[i] = false;

        taskIcons[i].transform.Find("IconToDo").gameObject.SetActive(true);
        taskIcons[i].transform.Find("IconDone").gameObject.SetActive(false);

        if (i != 0)
        {
            taskIcons[i].transform.parent.Find("IconMounted").gameObject.SetActive(false);
            tightenedItems[i] = false;
        }
    }


    public void IsTightened(int i)
    {
        tightenedItems[i] = true;
    }




    private void Reset()
    {
        mountedItems = new bool[] {false, false, false, false, false, false, false, false};
        tightenedItems = new bool[] { true, false, false, false, true, true, false, false };
    }

}
