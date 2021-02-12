using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class butonInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public string text;
    public GameObject txtGameObject;



    public void OnPointerEnter(PointerEventData eventData)
    {
        txtGameObject.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        txtGameObject.SetActive(false);
    }    
        
    
}
