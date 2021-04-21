using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class buildingConstruct : NetworkBehaviour
{
    GameObject menu;
    public Animator building;
    public Transform spawnSpot;
    public static int numberOfBuildableUnits = 5;
    public GameObject[] unit = new GameObject[numberOfBuildableUnits];
    public Sprite[] unitPicture = new Sprite[numberOfBuildableUnits];
    public float[] unitBuildTime = new float[numberOfBuildableUnits];
    public static int numberOfResources = 4;
    public static int numberOfUnits = 3;
    public string[] warningText = new string[numberOfResources];
    public int[] copperCost = new int[numberOfUnits];
    public int[] ironCost = new int[numberOfUnits];
    public int[] oilCost = new int[numberOfUnits];
    public int[] uraniumCost = new int[numberOfUnits];
    int cost;
    public float timer = 0;
    public GameObject[] queBars = new GameObject[7];
    public GameObject progressBar;

    public int[] unitQue = new int[7];

    public int numberOfUnitsCurrentlyInQue = 0;
    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("mainMenu");
    }
    void Update()
    {
        
        if (numberOfUnitsCurrentlyInQue != 0)
        {
            
            UpdateProgressBar(unitBuildTime[unitQue[0]], timer);
            
            timer += Time.deltaTime;
            if(unitBuildTime[unitQue[0]] - timer < 0)
            {
                BuildUnit(unitQue[0]);
            }
            UpdateQue();
        }

    }

    public void AddUnitToBuildQue(int unitID)
    {

        if (numberOfUnitsCurrentlyInQue != unitQue.Length)
        {
            
            if (RemoveResource(unitID))
            {
                unitQue[numberOfUnitsCurrentlyInQue] = unitID;
                
                numberOfUnitsCurrentlyInQue++;
            }


        }
        else
        {
            menu.GetComponent<hud>().addWarning("a de zuris? bato bre.");
            //play warning
        }

    }



    void GoDownByOne()
    {
        numberOfUnitsCurrentlyInQue--;
        for(int i = 0; i < unitQue.Length-1;i++)
        {
            unitQue[i] = unitQue[i + 1];
        }
        unitQue[unitQue.Length-1] = 0;
    }

    void UpdateProgressBar(float time, float progress)
    {
        float scaledProgress = 0 + (progress - 0) * (1 - 0) / (time - 0);
        //               low2 + (value - low1) * (high2 - low2) / (high1 - low1)
        float finalValue = scaledProgress * scaledProgress;
        progressBar.transform.localScale = new Vector3(finalValue, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
    }

    void UpdateQue()
    {
        for(int i =0;i<queBars.Length;i++)
        {
            
            if (unitQue[i] == 0)
            {
                queBars[i].SetActive(false);
            }
            else
            {
                queBars[i].GetComponent<Image>().sprite = unitPicture[unitQue[i]];
                queBars[i].SetActive(true);
            }
        }
    }
    bool RemoveResource(int id)
    {
        hud HUD = menu.GetComponent<hud>();

        for (int x = 0; numberOfResources > x; x++)
        {
            switch (x)
            {
                case 0:
                    cost = copperCost[id];

                    break;
                case 1:
                    cost = ironCost[id];

                    break;
                case 2:
                    cost = oilCost[id];

                    break;
                case 3:
                    cost = uraniumCost[id];

                    break;
                default:
                    Debug.Log("building placement remove resource error statement owerflow :,(");
                    break;

            }
            if ((HUD.resource[x] - cost) < 0)
            {


                menu.GetComponent<hud>().addWarning(warningText[x]);
                return false;
            }

        }
        for (int x = 0; numberOfResources > x; x++)
        {
            switch (x)
            {
                case 0:
                    cost = copperCost[id];

                    break;
                case 1:
                    cost = ironCost[id];

                    break;
                case 2:
                    cost = oilCost[id];

                    break;
                case 3:
                    cost = uraniumCost[id];

                    break;
                default:
                    Debug.Log("building placement remove resource error statement owerflow :,(");
                    break;

            }
            HUD.resource[x] -= cost;
        }
        return true;
    }

    void BuildUnit(int unitID)
    {
        timer = 0;
        
        cmdBuildUnit(unitID, spawnSpot.position + new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)), spawnSpot.rotation);

        GoDownByOne();
        if (building != null)
        {
            building.Play("spawn");
        }
        //animacija za spawn ovde
    }

    [Command] void cmdBuildUnit(int unitID,Vector3 spawnPosition,Quaternion spawnRotation)
    {
   
        GameObject instanciatedUnit = Instantiate(unit[unitID], spawnPosition, spawnRotation);
        instanciatedUnit.GetComponent<TIDsync>().teamColor = gameObject.GetComponent<teamIdentefier>().teamColor;
        instanciatedUnit.GetComponent<TIDsync>().teamID = gameObject.GetComponent<teamIdentefier>().teamID;

        NetworkServer.Spawn(instanciatedUnit,gameObject);
        Debug.Log(instanciatedUnit.name);

    }
}
