using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class menuMain : MonoBehaviour
{
    public AudioMixer main;
    /*public AudioMixer music;
    public AudioMixer gameEffects;
    public AudioMixer UIEffects;*/
    public GameObject transition;

    void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            transition.transform.parent.gameObject.SetActive(true);
            StartCoroutine(PlayTransitionIn());
        }
    }


    public void Play()
    {
        Time.timeScale = 1;
        StartCoroutine(PlayTransitionOut());

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator PlayTransitionOut()
    {
        transition.transform.parent.gameObject.SetActive(true);
        transition.GetComponent<Animator>().Play("out");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    IEnumerator PlayTransitionIn()
    {
        transition.transform.parent.gameObject.SetActive(true);
        transition.GetComponent<Animator>().Play("in");
        yield return new WaitForSeconds(3);
        transition.transform.parent.gameObject.SetActive(false);
    }

    public void SetVolumeMain(float volume)
    {
        main.SetFloat("main", Mathf.Log10(volume) * 20);
    }

    public void SetVolumeMusic(float volume)
    {
        main.SetFloat("music", Mathf.Log10(volume) * 20);
    }
    public void SetVolumeGame(float volume)
    {
        main.SetFloat("game", Mathf.Log10(volume) * 20);
    }
    public void SetVolumeUI(float volume)
    {
        main.SetFloat("ui", Mathf.Log10(volume) * 20);
    }

}
