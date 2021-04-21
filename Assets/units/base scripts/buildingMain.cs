using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class buildingMain : MonoBehaviour
{


    public GameObject gui;

    public Renderer renderrer;
    public Material building_identefier;
    Material building_identefier2;

    public teamIdentefier selmir_kad_oboji_kosu;


    void Start()
    {

        
        if (building_identefier != null && selmir_kad_oboji_kosu != null)
        {
            
            building_identefier2 = Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));

            var materials = renderrer.sharedMaterials.ToList();
            materials.Add(building_identefier2);
            materials.Remove(building_identefier);
            renderrer.materials = materials.ToArray();

            building_identefier2.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
            building_identefier2.SetColor("_OutlineColor", selmir_kad_oboji_kosu.teamColor);
        }
        gui.SetActive(false);
    }

    void Update()
    {

        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && gui.activeSelf)
        {
            if (building_identefier2 != null && selmir_kad_oboji_kosu != null)
            {
                building_identefier2.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                building_identefier2.SetColor("_OutlineColor", selmir_kad_oboji_kosu.teamColor);
            }
            gui.SetActive(false);
        }
    }

    public void UpdateColor()
    {

        building_identefier2.SetColor("_OutlineColor", selmir_kad_oboji_kosu.teamColor);

    }

    public void Selected()
    {
        gui.SetActive(true);
        if (building_identefier2)
        {
            building_identefier2.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
            building_identefier2.SetColor("_OutlineColor", Color.white);
        }
        /*aktiviraj go oji ima gui(info)*/

    }

    public void destroy()
    {
        Destroy(gameObject);
    }

}
