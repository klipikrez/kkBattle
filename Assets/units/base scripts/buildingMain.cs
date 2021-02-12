using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buildingMain : MonoBehaviour
{


    public GameObject gui;


    void Start()
    {
        gui.SetActive(false);
    }

    void Update()
    {

        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && gui.activeSelf)
        {
           
            gui.SetActive(false);
        }
    }

    public void Selected()
    {
       
        gui.SetActive(true);
        /*aktiviraj go oji ima gui(info)*/

    }

    public void destroy()
    {
        Destroy(gameObject);
    }

}
