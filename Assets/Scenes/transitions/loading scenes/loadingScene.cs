using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror.multiplayerOverride;
using Mirror;
public class loadingScene : MonoBehaviour
{

    public GameObject loadingBar;

    public GameObject transition;

    // Start is called before the first frame update

    private networkManagerOverride room;
    public networkManagerOverride Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as networkManagerOverride;
        }
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsyncOperation(sceneName));
    }

    IEnumerator LoadAsyncOperation(string sceneName)
    {
        transition.transform.parent.gameObject.SetActive(true);
        transition.GetComponent<Animator>().Play("in");
        yield return new WaitForSeconds(1);
        //SceneManager.LoadScene(1);
        //Room.StartGame(SceneUtility.GetScenePathByBuildIndex(1));
        Room.StartServer();
        //Room.StartGame(SceneUtility.GetScenePathByBuildIndex(1));
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(sceneName);
        while (gameLevel.progress < 1)
        {
            Debug.Log(gameLevel.progress);
            loadingBar.transform.localScale = new Vector3(gameLevel.progress * 500f, loadingBar.transform.localScale.y, loadingBar.transform.localScale.z);
            yield return new WaitForEndOfFrame();
        }
    }

}
