using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teamIdentefier : MonoBehaviour
{
    public int teamID = 0;
    public float health = 100f;
    public float maxHealth;
    public Color teamColor = Color.red;
    public Outline outline;
    public buildingMain teamIDI;

    public float explosionSize = 1f;

    public unitHealthBar bar;

    public bool isFlying = false;


    void Start()
    {
        maxHealth = health;

        if (outline == null)
        {
            if (gameObject.GetComponent<Outline>() != null)
            {
                gameObject.GetComponent<Outline>().outlineColor = teamColor;
            }
        }
        else
        {
            outline.outlineColor = teamColor;
        }
    }

    public void UpdateColor()
    {
        teamIDI.UpdateColor();
    }

    public void TakeDamage(float amount)
    {
        if (health - amount > 0)
        {
            health -= amount;
            if (bar != null)
            {
                bar.Damaged();
            }
        }
        else
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        GameObject death = Instantiate(Instantiate(Resources.Load<GameObject>(@"death")), transform.position, transform.rotation);
        death.transform.localScale = new Vector3(explosionSize, explosionSize, explosionSize);
        Destroy(gameObject);
    }
}
