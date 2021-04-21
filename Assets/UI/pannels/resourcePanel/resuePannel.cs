using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resuePannel : MonoBehaviour
{
    public Animator panelAnim;

    public void toggleNewValue(bool value)
    {
    if(value)
        {
            panelAnim.Play("in");
        }
    else
        {
            panelAnim.Play("out");
        }
    }

}
