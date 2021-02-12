using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update



    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject != null)
        {

            if (other.transform.parent.gameObject.GetComponent<teamIdentefier>() != null)
            {



                if (other.transform.CompareTag("Player"))
                {

                    anim.SetBool("player", true);
                }

            }
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.transform.parent.gameObject.GetComponent<teamIdentefier>() != null)
        {

           

            if (other.transform.CompareTag("Player"))
            {

                anim.SetBool("player", false);
            }


        }

    }
}
