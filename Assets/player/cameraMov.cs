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
        float y = 0f;
        y += Input.GetAxis("Mouse ScrollWheel") + Input.GetAxis("zoom");
        if (transform.position.y > maxHeight && y < 0f) y = 0;
        else if (transform.position.y < minHeight && y > 0f) y = 0;






        Vector3 move = transform.up * -y;

        controller.Move(move * zoomSpeed * Time.deltaTime );

    }
    void Rotate()
    {
        float mouseX = 0;
        if (Input.GetMouseButton(2) || Input.GetKey("space"))
        {
            mouseX += Input.GetAxis("Mouse X");
            //float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

        }
        mouseX += Input.GetAxis("rotateCamera");
        float rotateY = mouseX * mouseSensetivity * Time.deltaTime;

        gameObject.transform.Rotate(0, rotateY, 0, Space.World);
    }
    /*********************************************************************************opcije?*/
    public void BorderPan(bool value)
    {
        enableBorderPan = value;
    }
}



