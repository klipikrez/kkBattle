using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progressBar : MonoBehaviour
{

    public Transform bar;

    float oldValue = 0f;
    float newValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateBar(float time)
    {
        oldValue += Time.deltaTime;
        //NewValue = (((OldValue - OldMin) * (NewMax - NewMin)) / (OldMax - OldMin)) + NewMin

        newValue = (((oldValue - 0) * (1 - 0)) / (time - 0)) + 0;



        bar.transform.localScale = new Vector3(newValue, 1, 1);
        if (oldValue > time)
            oldValue = 0;
        //Debug.Log(oldValue + "  " + newValue + "  " + time);
    }

}


