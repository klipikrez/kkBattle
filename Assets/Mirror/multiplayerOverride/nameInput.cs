using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class nameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;

    

    public string username;

    
    void Start()
    {
        if(PlayerPrefs.GetString("username") != null)
        {
            nameInputField.text = PlayerPrefs.GetString("username");
        }

    }

    public void SaveName()
    {
        if (PlayerPrefs.GetString("username") != null)
        {
            username = PlayerPrefs.GetString("username");

        }


    }

    public void SetName(string name)
    {
        PlayerPrefs.SetString("username", name);
    }
}
