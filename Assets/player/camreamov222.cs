using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camreamov222 : MonoBehaviour
{
    public float mouseSensetivity = 200f;
    bool goToStartingRotation = true;


    public Quaternion rotationMin = Quaternion.Euler(new Vector3(-50f, 0f, 0f));
    public Quaternion rotationMax = Quaternion.Euler(new Vector3(50f, 0f, 0f));

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //2. while left mouse button held
        if (Input.GetKey("space") || Input.GetMouseButton(2))
        {

            float mouseY = Input.GetAxis("Mouse Y");

            float rotateX = -mouseY * mouseSensetivity * Time.deltaTime;

            if (transform.localRotation.x + rotateX /100f < 0)
            {
                transform.localRotation = new Quaternion(0, 0, 0, 1);

            }
            else
            {


                if (transform.localRotation.x + rotateX/100f > 0.4)
                {
                    transform.localRotation = new Quaternion(0.4f, 0, 0, 0.9f);

                }
                else
                {
                    gameObject.transform.Rotate(rotateX, 0, 0, Space.Self);
                }
            }


           



            goToStartingRotation = false;



        }

        //3. when mouse button comes up
        if (Input.GetKeyUp("space") || Input.GetMouseButtonUp(2))
        {
            //gameObject.transform.Rotate(0, rotate, 0, Space.World);
            goToStartingRotation = true;


        }

        if (goToStartingRotation)
        {
            Quaternion rotation = Quaternion.Euler(45, 0, 0);
            Quaternion currentRot = transform.localRotation;

            transform.localRotation = Quaternion.Slerp(currentRot, rotation, 1f * Time.deltaTime);

        }

    }
}
