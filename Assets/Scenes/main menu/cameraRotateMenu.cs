using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotateMenu : MonoBehaviour
{


    public Quaternion frontRot;

    public  Quaternion backRot;

    public Quaternion defaultRot;

    void Start()
    {
        defaultRot = frontRot;    
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, defaultRot, 3f * Time.deltaTime);
    }

    public void TurnBack()
    {
        defaultRot = backRot;

    }

    public void TurnFront()
    {
        defaultRot = frontRot;

    }

}
