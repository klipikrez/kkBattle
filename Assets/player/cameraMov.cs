using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class cameraMov : MonoBehaviour
{

    public float movSpeed = 20f;
    public float zoomSpeed = 100f;

    public float rotateXMax = -40f;
    public float rotateXMin = 0f;

    public float panBorderThicc = 10f;

    public CharacterController controller;

    public float maxHeight = 10f;
    public float minHeight = 1.7f;

    public float mouseSensetivity = 20f;

    public bool goToStartingRotation = false;
    /****ignore****/
    float xp = 0;
    float zp = 0;

    public bool enableBorderPan;

    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        Zoom();
        Move();
        Rotate();
    }
    
    void Move() 
    {
        if(enableBorderPan)
            ScreenEdgePan();

        float xm = Input.GetAxis("Horizontal");
        float zm = Input.GetAxis("Vertical");

        float x = xm + xp;
        float z = zm + zp;

        Vector3 move = transform.right * x + transform.forward * z;

        //transform.position = transform.position + (move * movSpeed * Time.deltaTime);
        controller.Move(move * movSpeed * Time.deltaTime * transform.position.y);

    }

    void ScreenEdgePan()
    {
        zp = 0;
        xp = 0;
        if (Input.mousePosition.y >= Screen.height - panBorderThicc)
            zp = 1;

        if (Input.mousePosition.y <= panBorderThicc)
            zp = -1;

        if (Input.mousePosition.x >= Screen.width - panBorderThicc)
            xp = 1;

        if (Input.mousePosition.x <= panBorderThicc)
            xp = -1;



    }

    void Zoom()
    {

        float y = Input.GetAxis("Mouse ScrollWheel");

        if (transform.position.y > maxHeight && y < 0f) y = 0;
        else if (transform.position.y < minHeight && y > 0f) y = 0;






        Vector3 move = transform.up * -y;

        controller.Move(move * zoomSpeed * Time.deltaTime );

    }
    void Rotate()
    {
        //1. when left mouse button clicked (but not released)
        /*
        if (Input.GetMouseButtonDown(2))
        {

        }
        */

        //2. while left mouse button held
        if (Input.GetMouseButton(2))
        {
            goToStartingRotation = false;
            float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
            //float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

            float rotateY = mouseX;
            //float rotateX = -mouseY;
            //Debug.Log(transform.localRotation.x);
            /*
            if (transform.rotation.x * 100f + rotateX > rotateXMin)
            {
                //rotateX = rotateXMin/100f - transform.localRotation.x;
                rotateX = 0;
            }
            if(transform.rotation.x * 100f + rotateX < rotateXMax)
            {
                //rotateX = rotateXMax/100f - transform.localRotation.x;
                rotateX = 0;
            }
            */
            //gameObject.transform.Rotate(rotateX, 0, 0, Space.Self);
            gameObject.transform.Rotate(0, rotateY, 0, Space.World);

        }
        /*
        //3. when mouse button comes up
        if (Input.GetMouseButtonUp(2))
        {
            //gameObject.transform.Rotate(0, rotate, 0, Space.World);
            goToStartingRotation = true; 
        }

        if(goToStartingRotation)
        {
            Quaternion rotation =  Quaternion.Euler(0, 0, 0);
            Quaternion currentRot = transform.rotation;

            transform.rotation = Quaternion.Slerp(currentRot, rotation, 1f *Time.deltaTime);
            
        }
        */
    }
    /*********************************************************************************opcije?*/
    public void BorderPan(bool value)
    {
        enableBorderPan = value;
    }
}



