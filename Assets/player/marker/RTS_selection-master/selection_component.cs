using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selection_component : MonoBehaviour
{
    Outline outline;
    Color outlineCurrentColor;
    // Start is called before the first frame update
    void Start()
    {
        outline = gameObject.GetComponent<Outline>();
        //GetComponent<Renderer>().material.color = Color.red;
        outlineCurrentColor = outline.outlineColor;
        gameObject.GetComponent<gass>().Selected();
        outline.OutlineColor = Color.white;

        //gameObject.GetComponent<Outline>().enabled = false;
        //gameObject.GetComponent<Outline1>().enabled = true;
    }

    private void OnDestroy()
    {
        //GetComponent<Renderer>().material.color = Color.white;
        gameObject.GetComponent<gass>().Deselected();


        if (outline != null)
        {
            outline.OutlineColor = outlineCurrentColor;
        }
            //gameObject.GetComponent<Outline>().enabled = true;
        //gameObject.GetComponent<Outline1>().enabled = false;
    }
}
