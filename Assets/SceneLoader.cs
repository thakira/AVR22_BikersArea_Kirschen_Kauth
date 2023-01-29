using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);

        scene = SceneManager.LoadScene(1, parameters);
        Debug.Log("Load 1 of scene2: " + scene.name);
        scene = SceneManager.LoadScene(2, parameters);
        Debug.Log("Load 2 of scene3: " + scene.name);
        scene = SceneManager.LoadScene(3, parameters);
        Debug.Log("Load 3 of scene4: " + scene.name);
        scene = SceneManager.LoadScene(4, parameters);
        Debug.Log("Load 4 of scene5: " + scene.name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
