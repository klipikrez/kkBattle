using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject panel;

    public GameObject[] scriptsToDisable = new GameObject[10];
    GameObject[] instanicableScriptsToDesable;

    public 
    void Start()
    {
        panel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (panel.activeInHierarchy)
            {
                ContinueGame();
                
            }
            else
            {
                PauseGame();
                
            }
        }
    }
    public void PauseGame()
    {        
        panel.SetActive(true);
        Time.timeScale = 0;
        DisableScripts(false);
        //Disable scripts that still work while timescale is set to 0
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
        DisableScripts(true);
        //enable the scripts again
    }

    public void DisableScripts(bool value)
    {
        //Disable another script as soon as your press Play

        foreach (var gameObject in scriptsToDisable)
        {

            var components = gameObject.GetComponents<Component>();

            foreach (var component in components)
            {
                var behaviour = component as Behaviour;
                if (behaviour) behaviour.enabled = value;
            }
        }


        instanicableScriptsToDesable = GameObject.FindGameObjectsWithTag("building");

        foreach (var gameObject in instanicableScriptsToDesable)
        {
            var components = gameObject.GetComponent<buildingConstruct>();
            if (components != null)
            {
                components.enabled = false;
            }
            /*
            var components = gameObject.GetComponents<Component>();

            foreach (var component in components)
            {
                var behaviour = component as Behaviour;
                if (behaviour) behaviour.enabled = value;
            }
            */
        }

    }
}
