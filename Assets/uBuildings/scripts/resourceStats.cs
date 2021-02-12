using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resourceStats : MonoBehaviour
{
    public GameObject oilWell;
    mainResource MainResource;

    public Text left;
    public Text rate;

    public string oilLeftName = "oil in reservoar";
    public string oilRateName = "oil extraction rate";

    void Start()
    {
        MainResource = oilWell.GetComponent<mainResource>();
    }

    void Update()
    {
        UpdateOilGui();
    }
    public void UpdateOilGui()
    {
        //oilLeft = MainResource.AmountOfResource;
        //oilRate = MainResource.resourceExtractionRate;
        //bonus = MainResource.bonus;
        if (left != null)
        {
            left.text = oilLeftName + " " + (int)MainResource.AmountOfResource;
        }
        if (rate != null)
        {
            rate.text = oilRateName + " " + (MainResource.resourceExtractionRate * MainResource.bonus);
        }
    }

    public void setResource(string leftr, string rater)
    {
        oilLeftName = leftr;
        oilRateName = rater;
    }

}
