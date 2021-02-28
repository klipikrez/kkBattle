using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teamIdentefier : MonoBehaviour
{
    public int teamID = 0;
    public float health = 100f;
    public Color teamColor = Color.red;

    public bool isFlying = false;


    void Start()
    {
        if (gameObject.GetComponent<Outline>() != null)
        gameObject.GetComponent<Outline>().outlineColor = teamColor;
    }

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
