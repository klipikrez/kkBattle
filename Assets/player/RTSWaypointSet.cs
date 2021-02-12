using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSWaypointSet : MonoBehaviour
{
    public GameObject waypoint;
    public GameObject selectBox;
    selected_dictionary playerScript;

    public LayerMask ignore;

    void Start()
    {
        playerScript = selectBox.GetComponent<selected_dictionary>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire2"))
            PutCoin(Input.mousePosition);

        
    }

    public void PutCoin(Vector2 mousePosition)
    {



        

        RaycastHit hit = RayFromCamera(mousePosition, 1000.0f);
        //Debug.Log(playerScript.allBuilders + "    " + hit.collider.tag);
        //Debug.Log(hit.collider.tag + playerScript.allBuilders);
        if (hit.collider.CompareTag("blueprint") && playerScript.allBuilders)
        {
            waypoint.transform.position = hit.collider.transform.position;
            waypoint.SetActive(true/*52*/);
            gameObject.transform.parent.gameObject.GetComponent<selected_dictionary>().SetDestination(waypoint.transform,hit.collider.gameObject);
        }
        if (hit.collider.tag.Equals("canGoHere"))
        {
            waypoint.transform.position = hit.point;
            waypoint.SetActive(true/*52*/);
            gameObject.transform.parent.gameObject.GetComponent<selected_dictionary>().SetDestination(waypoint.transform,null);
        }

        //auga

    }

    public RaycastHit RayFromCamera(Vector3 mousePosition, float rayLength)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Physics.Raycast(ray, out hit, rayLength, ignore);
        return hit;
    }
}
