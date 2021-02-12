using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selected : MonoBehaviour
{
    public float rotateSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * (rotateSpeed / 4f) * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.right * (rotateSpeed/3f) * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.forward * (rotateSpeed) * Time.deltaTime, Space.Self);

    }

    public void Inactivate()
    {
        gameObject.SetActive(false);
    }
}
