using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokeMenu : MonoBehaviour
{
    /*
    GameObject[] smoke;
    GameObject[] smokeSatck;
    */
    public List<GameObject> smokeList = new List<GameObject>();


    /*
    private void OnEnable()
    {
        smoke = GameObject.FindGameObjectsWithTag("smoke");
    }
    */
    private void Update()
    {
 
 
         foreach(GameObject fooObj in GameObject.FindGameObjectsWithTag("smoke"))
         {
                smokeList.Add(fooObj);
         }
    }

    public void DisableSmoke(bool state)
    {
        foreach (GameObject smok in smokeList)
        {
            smok.SetActive(state);
        }
    }
}
