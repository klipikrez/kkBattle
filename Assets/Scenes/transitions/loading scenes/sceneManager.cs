using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    GameObject SceneManager;

    public GameObject transition;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager = GameObject.Find("sceneLoader");
    }

    public void setScene(int sceneID)
    {
        StartCoroutine(transtion(SceneUtility.GetScenePathByBuildIndex(1)));
    }

    IEnumerator transtion(string name)
    {
        yield return new WaitForSeconds(1);
        SceneManager.GetComponent<sceneLoader>().SetSceneString(name);
    }

    public void setSceneString(string sceneName)
    {
        StartCoroutine(transtion(sceneName));
        
    }

}
