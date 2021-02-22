using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class menuMain : MonoBehaviour
{
    public AudioMixer mixer;

    void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            transition.Play("in");
        }
    }

    public Animator transition;
    public void Play()
    {
        StartCoroutine(PlayTransition());

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator PlayTransition()
    {
        transition.gameObject.SetActive(true);
        transition.Play("out");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("main", Mathf.Log10(volume) * 20);
    }

}
