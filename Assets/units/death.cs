using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class death : MonoBehaviour
{

    public float time = 7f;
    public float startDisolve = 0f;
    float count = 0;
    float count2 = 0;

    public Renderer[] bomb = new Renderer[2];

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Death");
    }

    void Update()
    {
        if (bomb != null)
        {
            count += Time.deltaTime;
            if (count > startDisolve)
            {
                count2 += Time.deltaTime;
                foreach (Renderer mat in bomb)
                {
                    mat.material.SetFloat("Vector1_84765A4C", ((time-startDisolve) - (count2)) / (time - startDisolve));
                }
            }
        }
    }



    IEnumerator Death()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
