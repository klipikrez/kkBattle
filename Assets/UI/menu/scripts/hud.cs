using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class hud : MonoBehaviour
{
    public float[] resource = new float[5];
    public float[] resoureCap = new float[5];
    public Text[] resourceText = new Text[5];
    public string[] resourceName = new string[5];
    public Animator warning;
    int number;
    public Text warningText;
    public bool[] canPlay = new bool[5];

    public Dictionary<int, string> warningTable = new Dictionary<int, string>();


    bool canPlayWarning = false;
    private void Awake()
    {
        updateResourceNumbers();

    }



    public void updateResourceNumbers()
    {
        for (int i = 0; resource.Length > i; i++)
        {
            IndividualNumberUpdate(i);
        }

    }

    void IndividualNumberUpdate(int i)
    {
        if (resource[i] != 0)
        {
            resourceText[i].text = resourceName[i] + " " + (int)resource[i];
        }
    }

    public void addResource(float amount,int i)
    {
        number = i;
        if (resource[number] + amount < resoureCap[number])
        {
            resource[number] += amount;
            canPlay[number] = true;
        }
        else
        {
            if (canPlay[number])
            {
                addWarning("!!!danger!!!\nwarning " + resourceName[number].ToString() + " storage is full\n!!!danger!!!");
                canPlay[number] = false;
            }
            resource[number] = resoureCap[number];
        }
        IndividualNumberUpdate(i);
    }



    public void addWarning(string warningString)
    {
        warning.SetBool("inUse", true);
        warningText.text = warningString;
        warning.Play("reset");
    }

}
