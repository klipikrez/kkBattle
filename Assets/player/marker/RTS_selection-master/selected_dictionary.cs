using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class selected_dictionary : MonoBehaviour
{
    bool ignoreThis = false;
    public bool allBuilders = true;

    public GameObject builderOptionsMenu;

    public float dist = 2f;
    public float rad = 8f;
    public float radAmp = 0.7f;
    public float distAmp = -0.011f;

    public float radTimes = 2f;
    public float distTimes = 2f;

    public Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> selectedTableBuilders = new Dictionary<int, GameObject>();

    public bool once;

    buildngPlacement BuildngPlacement;

    void Awake()
    {
        BuildngPlacement = GameObject.FindGameObjectWithTag("mainMenu").GetComponent<buildngPlacement>();
    }

    public void addSelected(GameObject go)
    {
        //Debug.Log(go.name);
        if (!go.transform.CompareTag("builder"))
        {
            allBuilders = false;
        }
        
            int id = go.GetInstanceID();

        if (!(selectedTable.ContainsKey(id)) && (go.gameObject.CompareTag("builder")) && (allBuilders))
        {
            selectedTableBuilders.Add(id, go);
            go.AddComponent<selection_component>();
            //Debug.Log("Added " + id + " to selected dict");        
        }


        if(!(selectedTable.ContainsKey(id)) && go.gameObject.CompareTag("unit"))
        {
            selectedTable.Add(id, go);
            go.AddComponent<selection_component>();
        }

        if (allBuilders)
        {
            SelectedBuilder();
        }
        else
        {
            if (once)
            {
                DeselectBuilders();
            }
            once = false;
        }

    }

    void DeselectBuilders()
    {
        //builderOptionsMenu.SetActive(false);
        foreach (KeyValuePair<int, GameObject> pair in selectedTableBuilders)
        {
            if (pair.Value != null)
            {
                Destroy(selectedTableBuilders[pair.Key].GetComponent<selection_component>());
            }
        }
        selectedTableBuilders.Clear();
    }

    public void SelectedBuilder()
    {
        /*enable the building screen*/
        builderOptionsMenu.SetActive(true);
    }
    public void DeselectedBuilder()
    {
        /*enable the building screen*/
        builderOptionsMenu.SetActive(false);
    }


    public void deselectAll()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            
            once = true;
            allBuilders = true;
            foreach (KeyValuePair<int, GameObject> pair in selectedTable)
            {
                if (pair.Value != null)
                {
                    Destroy(selectedTable[pair.Key].GetComponent<selection_component>());
                }
            }
            selectedTable.Clear();

            foreach (KeyValuePair<int, GameObject> pair in selectedTableBuilders)
            {
                if (pair.Value != null)
                {
                    Destroy(selectedTableBuilders[pair.Key].GetComponent<selection_component>());
                }
            }
            selectedTableBuilders.Clear();
            DeselectedBuilder();
        }
    }

    public void SetDestination(Transform destination, GameObject isBuildng)
    {

        
        if (!allBuilders)
        {
            //attackers
            if (!Input.GetKey(KeyCode.LeftShift))
            {

                RemoveAll(selectedTable);
            }

            if (!Input.GetKey("left ctrl"))
            {
                
                AddLast(selectedTable, destination, null);
            }
            else
            {
                AddFirst(selectedTable, destination, null);
            }


        }
        else
        {
            
            if (!BuildngPlacement.inUse && !ignoreThis)
            {
                //builders
                if (!Input.GetKey(KeyCode.LeftShift))
                {

                    RemoveAll(selectedTableBuilders);
                }

                if (!Input.GetKey("left ctrl"))
                {
                    AddLast(selectedTableBuilders, destination, isBuildng);
                }
                else
                {
                    AddFirst(selectedTableBuilders, destination, isBuildng);
                }
            }
            else
            {
                
                if (!Input.GetKey("left ctrl"))
                {
                    AddLast(selectedTableBuilders, destination, isBuildng);
                }
                else
                {
                    AddFirst(selectedTableBuilders, destination, isBuildng);
                }
            }
            ignoreThis = BuildngPlacement.inUse;
        }
 
    }

    public void SetTarget(GameObject target)
    {

    }


    void RemoveAll(Dictionary<int, GameObject> dictionary)
    {
        foreach (KeyValuePair<int, GameObject> pair in dictionary)
        {
            if (pair.Value != null)
            {
                dictionary[pair.Key].GetComponent<gass>().RemoveAll();
                
            }
        }
    }

    void AddFirst(Dictionary<int, GameObject> dictionary,Transform destination, GameObject isBuildng)
    {
        int i = 1;
        foreach (KeyValuePair<int, GameObject> pair in dictionary)
        {
            if (pair.Value != null)
            {

                dictionary[pair.Key].GetComponent<gass>().AddFirst(RandomPos(destination, i), isBuildng);
                i++;
            }
        }
    }

    void AddLast(Dictionary<int, GameObject> dictionary, Transform destination, GameObject isBuildng)
    {
        int i = 1;
        foreach (KeyValuePair<int, GameObject> pair in dictionary)
        {
            if (pair.Value != null)
            {

                dictionary[pair.Key].GetComponent<gass>().AddLast(RandomPos(destination, i), isBuildng);
                i++;
            }
        }
    }




    public void SelectedBuilding(GameObject go)
    {


        go.GetComponent<buildingMain>().Selected();

        DeselectedBuilder();
    }



    Vector3 RandomPos(Transform start, int unitNumber)
    {
         /*****************************************************************************ovde napravi spiralu aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa*/

        float angle = unitNumber * Mathf.PI * (dist + unitNumber*distAmp) / (rad);
        Vector3 newPos = start.transform.position + (new Vector3(Mathf.Cos(angle) * (rad + unitNumber*radAmp), 2, Mathf.Sin(angle) * (rad + unitNumber * radAmp)));


        return newPos;
    }


}
