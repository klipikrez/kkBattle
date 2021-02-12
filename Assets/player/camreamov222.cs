using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camreamov222 : MonoBehaviour
{
    public float mouseSensetivity = 200f;
    bool goToStartingRotation = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //2. while left mouse button held
        if (Input.GetMouseButton(2))
        {
            goToStartingRotation = false;
            //float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

            float rotateX = -mouseY;
            if (transform.localRotation.x <=0 && rotateX<0)
            {
                rotateX = 0;
            }
            if (transform.localRotation.x > 0.4f && rotateX > 0)
            {
                rotateX = 0;
            }
            //Debug.Log(transform.localRotation.x);
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
            gameObject.transform.Rotate(rotateX, 0, 0, Space.Self);
            //gameObject.transform.Rotate(0, rotateY, 0, Space.World);

        }

        //3. when mouse button comes up
        if (Input.GetMouseButtonUp(2))
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
