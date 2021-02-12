using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marker : MonoBehaviour
{


    public float rotateSpeed = 50f;

    public Transform unit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.Self);
        //SetDestination();

    }



    /*
    public void SetDestination()
    {


        unit.GetComponent<selected_dictionary>().SetDestination(gameObject.transform);
        
    }
    */
}

/*
public class PlayerScript: MonoBehaviour {
    public float Health = 100.0f;
}
 */


/*
 public class Accessor : MonoBehaviour {
    void Start()
    {
        GameObject thePlayer = GameObject.Find("ThePlayer");
        PlayerScript playerScript = thePlayer.GetComponent<PlayerScript>();
        playerScript.Health -= 10.0f;
    }
}
 */