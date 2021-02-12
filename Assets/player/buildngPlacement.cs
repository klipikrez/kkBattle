using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buildngPlacement : MonoBehaviour
{
    
    
    public LayerMask ignore;
    public static int numberOfBuildings = 6;
    public GameObject[] BuildingsBlueprintsQ = new GameObject[numberOfBuildings];
    public GameObject[] BuildingsPrefabsQ = new GameObject[numberOfBuildings];
    public Vector3[] BuildingsStartLocationOffsetQ = new Vector3[numberOfBuildings];
    public Vector3[] BuildingDimensionsQ = new Vector3[numberOfBuildings];
    public LayerMask[] BuildingLayersQ = new LayerMask[numberOfBuildings];
    public string[] warningText = new string[numberOfBuildings];
    public int numberOfResources = 4;
    public int[] copperCost = new int[numberOfBuildings];
    public int[] ironCost = new int[numberOfBuildings];
    public int[] oilCost = new int[numberOfBuildings];
    public int[] uraniumCost = new int[numberOfBuildings];

    public LayerMask DestructableLayers;


    GameObject selectBox;

    public float rotaeBuildingSencetivity = 5f;

    public int number;

    public bool inUse;

    Animator otherAnimator;

    int i = 0;

    public float panBorderThicc = 20f;
    //public GameObject builderMenu;



    int cost=0;





    void Start()
    {

        
        selectBox = GameObject.FindGameObjectWithTag("selectBox");
        
        //Debug.Log("Tree Instances:" + TreeInstances.Count);
    }

    void Update()
    {


        if (inUse)
        {

            if (i == 0)
            {
                //iskluci skripte
                i++;
                gameObject.GetComponent<pause>().DisableScripts(false);
            }
            //skripta da objekat prati mis.   broj odredjuje koji Transform aktivno prati mis

            RaycastHit hit = RayFromCamera(Input.mousePosition, 1000.0f);
            BuildingsBlueprintsQ[number].transform.position = hit.point;
            if (!BuildingsBlueprintsQ[number].activeSelf)
            {
                BuildingsBlueprintsQ[number].SetActive(true);
            }

            bool canPlaceHere = Physics.CheckBox(hit.point + BuildingsStartLocationOffsetQ[number], BuildingDimensionsQ[number], BuildingsBlueprintsQ[number].transform.rotation, ~BuildingLayersQ[number]);
            if (!canPlaceHere)
            {

                otherAnimator.Play("canGoHere");
            }
            else
            {
                otherAnimator.Play("cantGoHere");

            }
            RotateHologram();
            if (Input.GetMouseButtonDown(0))
            {
                float remap = map(panBorderThicc, 0, 100, 0, Screen.height);
                if (Input.mousePosition.y <= remap /*&& builderMenu.activeSelf && !EventSystem.current.IsPointerOverGameObject()*/)
                {
                    BuildingsBlueprintsQ[number].SetActive(false);
                    inUse = false;
                    i = 0;
                    gameObject.GetComponent<pause>().DisableScripts(true);
                    //builderMenu.SetActive(false);
                }
                else
                {

                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        BuildingsBlueprintsQ[number].SetActive(false);
                        inUse = false;
                        i = 0;
                        gameObject.GetComponent<pause>().DisableScripts(true);
                        //builderMenu.SetActive(false);
                    }
                    PlaceBuilding();
                }




            }
            else
            if (Input.GetMouseButtonDown(1))
            {
                BuildingsBlueprintsQ[number].SetActive(false);
                inUse = false;
                i = 0;
                //aktivira opet sve skripte sto su iskljucene ⊙⊙ on glidi :)
                gameObject.GetComponent<pause>().DisableScripts(true);
                //builderMenu.SetActive(false);
            }
        }

    }
    public void PlaceBuilding()
    {




        bool canPlaceHere = BoxCast();
        if (!canPlaceHere && !EventSystem.current.IsPointerOverGameObject())
        {
            if (RemoveResources())
            {
                //destroy trees and stuff

                foreach (Collider col in BoxCollect())
                {
                    //Debug.Log(col.gameObject.tag);
                    if (col.gameObject.CompareTag("destructableTerrain"))
                    {


                        Destroy(col.gameObject);

                    }
                }

                //stavi menu u taj variavble
                GameObject building = Instantiate(BuildingsPrefabsQ[number], BuildingsBlueprintsQ[number].transform.position, BuildingsBlueprintsQ[number].transform.rotation);

                if (building.GetComponent<mainResource>() != null)
                {
                    building.GetComponent<mainResource>().mainMenu = gameObject;
                }
                selectBox.GetComponent<selected_dictionary>().SetDestination(building.transform, building);
            }
            else
            {
                BuildingsBlueprintsQ[number].SetActive(false);
            }
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            BuildingsBlueprintsQ[number].SetActive(false);
        }
    }

    bool RemoveResources()
    {
        
        hud HUD = gameObject.GetComponent<hud>();

        for (int x = 0; numberOfResources > x; x++)
        {
            switch (x)
            {
                case 0:
                    cost = copperCost[number];
                    
                    break;
                case 1:
                    cost = ironCost[number];
                    
                    break;
                case 2:
                    cost = oilCost[number];
                   
                    break;
                case 3:
                    cost = uraniumCost[number];
                    
                    break;
                default:
                    Debug.Log("building placement remove resource error statement owerflow :,(");
                    break;

            }
            if ((HUD.resource[x] - cost) < 0)
            {
                
                
                gameObject.GetComponent<hud>().addWarning(warningText[x]);
                return false;
            }

        }
        for (int x = 0; numberOfResources > x; x++)
        {
            switch (x)
            {
                case 0:
                    cost = copperCost[number];

                    break;
                case 1:
                    cost = ironCost[number];

                    break;
                case 2:
                    cost = oilCost[number];

                    break;
                case 3:
                    cost = uraniumCost[number];

                    break;
                default:
                    Debug.Log("building placement remove resource error statement owerflow :,(");
                    break;

            }
            HUD.resource[x] -= cost;
        }
        return true;
    }

    public void RotateHologram()
    {

        float rotation = Input.GetAxis("rotateBuilding");
        BuildingsBlueprintsQ[number].transform.Rotate(0, -rotation * rotaeBuildingSencetivity, 0, Space.World);
    }

    public void PutBuildingDown(int buildingNumber)
    {
        number = buildingNumber;
        inUse = true;
        otherAnimator = BuildingsBlueprintsQ[number].GetComponent<Animator>();
    }

    public RaycastHit RayFromCamera(Vector3 mousePosition, float rayLength)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Physics.Raycast(ray, out hit, rayLength, ignore);
        return hit;
    }

    public bool BoxCast()
    {
        RaycastHit hit = RayFromCamera(Input.mousePosition, 1000.0f);
        bool canPlaceHere = Physics.CheckBox(hit.point + BuildingsStartLocationOffsetQ[number], BuildingDimensionsQ[number], BuildingsBlueprintsQ[number].transform.rotation, ~BuildingLayersQ[number]);
        return canPlaceHere;
    }

    public Collider[] BoxCollect()
    {
        RaycastHit hit = RayFromCamera(Input.mousePosition, 1000.0f);
        Collider[] hitColliders = Physics.OverlapBox(hit.point + BuildingsStartLocationOffsetQ[number], BuildingDimensionsQ[number] , BuildingsBlueprintsQ[number].transform.rotation, DestructableLayers);
        //Debug.Log(hitColliders);
        return hitColliders;
    }



    float map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


    private void OnDrawGizmos()
    {

        //bool canPlaceHere = Physics.CheckBox(hit.point + new Vector3(0, 3, 0), checkRange, BuildingsQ[number].transform.rotation, canGoOn);
        RaycastHit hit = RayFromCamera(Input.mousePosition, 1000.0f);
        Gizmos.color = Color.magenta;
        //Gizmos.matrix = Matrix4x4.TRS(hit.point + new Vector3(0, 3, 0), BuildingsQ[number].transform.rotation, checkRange);
        Gizmos.DrawWireCube(hit.point + BuildingsStartLocationOffsetQ[number], BuildingDimensionsQ[number] * 2);
    }
    
}
