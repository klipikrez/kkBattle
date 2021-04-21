using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    // Start is called before the first frame update

    string nextScene;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetScene(int sceneNumber)
    {
        nextScene = GetSceneNameFromBuildIndex(sceneNumber);
        Debug.Log(nextScene);
        SceneManager.LoadScene(1);
    }

    public void SetSceneString(string sceneName)
    {
        nextScene = sceneName;
        Debug.Log(nextScene);
        SceneManager.LoadScene(1);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameObject loadingScript = GameObject.Find("loading bar");
            loadingScript.GetComponent<loadingScene>().LoadScene(nextScene);
            //StartCoroutine(waitForABit());

        }
    }

    /*
    IEnumerator waitForABit()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject loadingScript = GameObject.Find("loading bar");
        loadingScript.GetComponent<loadingScene>().LoadScene(nextScene);
    }*/

    public static string GetSceneNameFromBuildIndex(int index)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(index);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

        return sceneName;
    }
}
