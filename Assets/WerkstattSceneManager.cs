using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class WerkstattSceneManager : MonoBehaviour
{
    public static WerkstattSceneManager instance;

    // WerstattSceneManager.instance.IsTightened(1);

    [SerializeField, Tooltip("Greifarm Socket, der überprüft werden soll.")]
    private XRSocketInteractor grabSocketInteractor;

    [SerializeField, Tooltip("Reihenfolge beachten! -yellow-blue-red-pink-black-grey-green-")] 
    private Sprite[] bikeFrameImages;
    
    [SerializeField] private Image currentTaskImage;
    [SerializeField] private Image[] taskThumbnails;
    [SerializeField] private GameObject[] taskIcons;
    [SerializeField] private GameObject[] bikeItems;
    [SerializeField] private GameObject[] bikeFrames;

    private bool[] mountedItems = { false, false, false, false, false, false, false, false };
    private bool[] tightenedItems = { true, false, false, false, true, true, false, false };

    private int[] tasks;

    private int lenkerInWorkshop = 0;
    private int radInWorkshop = 0;
    private int zahnradInWorkshop = 0;
    private int pedaleInWorkshop = 0;
    private bool[] framesInWorkshop = { false, false, false, false, false, false, false }; //-yellow-blue-red-pink-black-grey-green-

    // Buttons
    [SerializeField] private TextMeshProUGUI labelAnzahlLenker;
    [SerializeField] private TextMeshProUGUI labelAnzahlRad;
    [SerializeField] private TextMeshProUGUI labelAnzahlZahnrad;
    [SerializeField] private TextMeshProUGUI labelAnzahlPedale;

    private bool[] frameOrderAdded = { false, false, false, false, false, false, false };
    [SerializeField] private TextMeshProUGUI[] frameOrderLabels;

    private string[] colors = { "yellow", "blue", "red", "pink", "black", "grey", "green" };


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

    public void OrderRad()
    {
        int anzahl = int.Parse(labelAnzahlRad.text);

        if (anzahl == 2)
        {
            bikeItems[1].SetActive(true);
            bikeItems[2].SetActive(true);
        }
        else if (anzahl == 1)
        {
            if (!bikeItems[1].activeSelf)
            {
                bikeItems[1].SetActive(true);
            }
            else
            {
                bikeItems[2].SetActive(true);
            }
        }

        labelAnzahlRad.text = "0";
    }

    public void OrderLenker()
    {
        int anzahl = int.Parse(labelAnzahlLenker.text);

        if (anzahl > 0)
        {
            bikeItems[0].SetActive(true);
        }

        labelAnzahlLenker.text = "0";
    }

    public void OrderZahnrad()
    {
        int anzahl = int.Parse(labelAnzahlZahnrad.text);

        if (anzahl == 2)
        {
            bikeItems[3].SetActive(true);
            bikeItems[4].SetActive(true);
        }
        else if (anzahl == 1)
        {
            if (!bikeItems[3].activeSelf)
            {
                bikeItems[3].SetActive(true);
            }
            else
            {
                bikeItems[4].SetActive(true);
            }
        }

        labelAnzahlZahnrad.text = "0";
    }


    public void OrderPedale()
    {
        int anzahl = int.Parse(labelAnzahlPedale.text);

        if (anzahl == 2)
        {
            bikeItems[5].SetActive(true);
            bikeItems[6].SetActive(true);
        }
        else if (anzahl == 1)
        {
            if (!bikeItems[5].activeSelf)
            {
                bikeItems[5].SetActive(true);
            }
            else
            {
                bikeItems[6].SetActive(true);
            }
        }

        labelAnzahlPedale.text = "0";
    }

    public void OrderCounterAdd(TextMeshProUGUI t)
    {
        Debug.Log("CounterUp: " + t.text);

        int current = int.Parse(t.text);
        int newInt;

        if (current == 2)
        {
            newInt = current;
        }
        else
        {
            newInt = current + 1;
        }

        t.text = newInt.ToString();
    }
    public void OrderCounterRemove(TextMeshProUGUI t)
    {
        int current = int.Parse(t.text);
        int newInt;

        if (current == 0)
        {
            newInt = current;
        }
        else
        {
            newInt = current - 1;
        }

        t.text = newInt.ToString();
    }


    public void AddFrameOrder(int i)
    {
        if(frameOrderAdded[i])
        {
            frameOrderAdded[i] = false;
            frameOrderLabels[i].text = "0";
        }
        else
        {
            frameOrderAdded[i] = true;
            frameOrderLabels[i].text = "1";
        }
    }

    public void orderFrame()
    {

        for(int i=0; i<frameOrderAdded.Length; i++)
        {
            if(frameOrderAdded[i])
            {
                bikeFrames[i].SetActive(true);
                frameOrderAdded[i] = false;
                frameOrderLabels[i].text = "0";
            }
        }
    }


    private void Reset()
    {
        mountedItems = new bool[] {false, false, false, false, false, false, false, false};
        tightenedItems = new bool[] { true, false, false, false, true, true, false, false };
    }

}
