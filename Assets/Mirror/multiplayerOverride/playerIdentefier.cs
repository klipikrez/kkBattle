using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerIdentefier : MonoBehaviour
{
    public TMP_Text usernameText;

    public void updateUi(string name)
    {
        usernameText.text = name;
    }
}
