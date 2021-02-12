using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class global_selection : MonoBehaviour
{
    teamIdentefier selfTeamID;
    public LayerMask IgnoreMe;
    LayerMask IgnoreMe2;
    public GameObject waypoint;

    //IDictionary id_table;
    selected_dictionary selected_table;
    RaycastHit hit;

    public bool dragSelect;


    //Collider variables
    //=======================================================//

    MeshCollider selectionBox;
    Mesh selectionMesh;

    Vector3 p1;
    Vector3 p2;

    //the corners of our 2d selection box
    Vector2[] corners;

    //the vertices of our meshcollider
    Vector3[] verts;
    Vector3[] vecs;

    // Start is called before the first frame update
    void Start()
    {
        selfTeamID = gameObject.GetComponent<teamIdentefier>();
        //id_table = GetComponent<IDictionary>();
        selected_table = GetComponent<selected_dictionary>();
        dragSelect = false;
        IgnoreMe = ~IgnoreMe;
        IgnoreMe2 = ~IgnoreMe;
    }

    void OnEnable()
    {
        //menuOpen = false;
        dragSelect = false;
        p1 = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        //1. when left mouse button clicked (but not released)
        if (Input.GetMouseButtonDown(0))
        {
            waypoint.SetActive(false);
            p1 = Input.mousePosition;
        }

        //2. while left mouse button held
        if (Input.GetMouseButton(0))
        {
            if((p1 - Input.mousePosition).magnitude > 40)
            {
                dragSelect = true;
            }
        }

        //3. when mouse button comes up
        if (Input.GetMouseButtonUp(0))
        {
            //float fnal = 1 + (builderMenuScale - 1) * (Screen.height - 1) / (100 - 1);






            if (dragSelect == false) //single select
            {
                
                
                    Ray ray = Camera.main.ScreenPointToRay(p1);
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (Physics.Raycast(ray, out hit, 50000.0f, IgnoreMe) && (hit.transform.GetComponent<teamIdentefier>() == null || hit.transform.GetComponent<teamIdentefier>().teamID == selfTeamID.teamID))
                    {
                        /*if (hit.transform.CompareTag("unit"))
                        {

                            selected_table.SelectedBuilder(hit.transform.gameObject);
                        }*/

                        /*gleda daali si kliknuo na zgradu ili nes drugo*/
                        if ((hit.transform.CompareTag("blueprint") || hit.transform.CompareTag("building")) && !EventSystem.current.IsPointerOverGameObject())
                        {

                            selected_table.SelectedBuilding(hit.transform.gameObject);
                        }
                        else
                        {

                            if (Input.GetKey(KeyCode.LeftShift)) //inclusive select
                            {

                                selected_table.addSelected(hit.transform.gameObject);

                            }
                            else //exclusive selected
                            {
                                if (!EventSystem.current.IsPointerOverGameObject())
                                {
                                    selected_table.deselectAll();
                                }

                                selected_table.addSelected(hit.transform.gameObject);

                            }

                        }

                    }
                    else //if we didnt hit something
                    {
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            //do nothing
                        }
                        else
                        {
                            if (!EventSystem.current.IsPointerOverGameObject())
                            {
                                selected_table.deselectAll();
                            }
                        }
                    }
                }
            }
            else //marquee select
            {
                //lo1 1    high1 100    lo2 1    high2 screen.height
                //low2 + (value - low1) * (high2 - low2) / (high1 - low1)
                //float fnal = 1 + (builderMenuScale - 1) * (Screen.height - 1) / (100 - 1);


                p2 = Input.mousePosition;



                verts = new Vector3[4];
                vecs = new Vector3[4];
                int i = 0;

                corners = getBoundingBox(p1, p2);

                foreach (Vector2 corner in corners)
                {
                    Ray ray = Camera.main.ScreenPointToRay(corner);

                    if (Physics.Raycast(ray, out hit, 50000.0f, IgnoreMe2))
                    {
                        verts[i] = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        vecs[i] = ray.origin - hit.point;
                        Debug.DrawLine(Camera.main.ScreenToWorldPoint(corner), hit.point, Color.red, 1.0f);
                    }
                    i++;
                }

                //generate the mesh
                selectionMesh = generateSelectionMesh(verts, vecs);

                selectionBox = gameObject.AddComponent<MeshCollider>();
                selectionBox.sharedMesh = selectionMesh;
                selectionBox.convex = true;
                selectionBox.isTrigger = true;

                if (!Input.GetKey(KeyCode.LeftShift) && !EventSystem.current.IsPointerOverGameObject())
                {
                    selected_table.deselectAll();
                }

                Destroy(selectionBox, 0.02f);

            }//end marquee select

            dragSelect = false;

        }
       
    }
    
    private void OnGUI()
    {
        if(dragSelect == true)
        {

            var rect = Utils.GetScreenRect(p1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }
    
    //create a bounding box (4 corners in order) from the start and end mouse position
    Vector2[] getBoundingBox(Vector2 p1,Vector2 p2)
    {
        Vector2 newP1;
        Vector2 newP2;
        Vector2 newP3;
        Vector2 newP4;

        if (p1.x < p2.x) //if p1 is to the left of p2
        {
            if (p1.y > p2.y) // if p1 is above p2
            {
                newP1 = p1;
                newP2 = new Vector2(p2.x, p1.y);
                newP3 = new Vector2(p1.x, p2.y);
                newP4 = p2;
            }
            else //if p1 is below p2
            {
                newP1 = new Vector2(p1.x, p2.y);
                newP2 = p2;
                newP3 = p1;
                newP4 = new Vector2(p2.x, p1.y);
            }
        }
        else //if p1 is to the right of p2
        {
            if (p1.y > p2.y) // if p1 is above p2
            {
                newP1 = new Vector2(p2.x, p1.y);
                newP2 = p1;
                newP3 = p2;
                newP4 = new Vector2(p1.x, p2.y);
            }
            else //if p1 is below p2
            {
                newP1 = p2;
                newP2 = new Vector2(p1.x, p2.y);
                newP3 = new Vector2(p2.x, p1.y);
                newP4 = p1;
            }

        }

        Vector2[] corners = { newP1, newP2, newP3, newP4 };
        return corners;

    }

    //generate a mesh from the 4 bottom points
    Mesh generateSelectionMesh(Vector3[] corners, Vector3[] vecs)
    {
        Vector3[] verts = new Vector3[8];
        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 }; //map the tris of our cube

        for(int i = 0; i < 4; i++)
        {
            verts[i] = corners[i];
        }

        for(int j = 4; j < 8; j++)
        {
            verts[j] = corners[j - 4] + vecs[j - 4];
        }

        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = verts;
        selectionMesh.triangles = tris;

        return selectionMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        teamIdentefier team = other.gameObject.GetComponent<teamIdentefier>();

        if (!other.transform.CompareTag("Player") && ( team == null || team.teamID == selfTeamID.teamID ))
        {
            selected_table.addSelected(other.gameObject);
            //Debug.Log(other.transform.gameObject.tag);
        }
    }

    private void OnDisable()
    {
        dragSelect = false;
    }

}
