using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainResource : MonoBehaviour
{
    public GameObject mainMenu;

    public GameObject resourceGui;

    public float AmountOfResource = 1000f;
    public float resourceExtractionRate = 1f;
    public float resourceLoss = 0;
    public float bonus = 1;

    public int resourceID;

    void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("mainMenu");
    }

    void Update()
    {
        if (resourceGui != null)
        {
            ExtractResource(resourceExtractionRate);
        }
    }

    public void ExtractResource(float amount)
    {

        amount = amount * Time.deltaTime * bonus;
        AmountOfResource -= resourceLoss * Time.deltaTime;
        if (AmountOfResource > 0)
        {
            mainMenu.GetComponent<hud>().addResource(amount, resourceID);
            AmountOfResource -= amount;
        }
        else
        {
            //Debug.Log("prazno burazzzzzzz");
            AmountOfResource = 0f;
        }
    }

    public void SetResource(string leftr, string rater)
    {
        resourceGui.gameObject.GetComponent<resourceStats>().setResource(leftr, rater);

        ///resourceGui

    }


    public void destroy()
    {
        Destroy(gameObject);
    }

}
