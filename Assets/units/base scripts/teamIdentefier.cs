using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teamIdentefier : MonoBehaviour
{
    public int teamID = 0;
    public float health = 100f;

    public bool isFlying = false;




    public void TakeDamage(float amount)
    {
        if (health - amount > 0)
        {
            health -= amount;
        }
        else
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
