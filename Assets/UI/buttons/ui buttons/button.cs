using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour
{

    public Image burronIdentefier;
    public Color dimCol;
    public Color lightCol;
    void dim()
    {
        burronIdentefier.color = dimCol;
    }

    void light()
    {
        burronIdentefier.color = lightCol;

    }

    public void ChangeIdentefierColor(bool val)
    {
        if(val)
        {
            dim();
            
        }
        else
        {
            light();
        }
    }


}
