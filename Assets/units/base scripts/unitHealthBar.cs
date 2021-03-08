using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitHealthBar : MonoBehaviour
{

    public teamIdentefier team;
    public GameObject health;
    public GameObject backGround;
    public teamIdentefier playerTeam;

    void Start()
    {
        //gets players team id to compare with unit team id
        GameObject selectBox = GameObject.FindGameObjectWithTag("selectBox");
        playerTeam = selectBox.GetComponent<teamIdentefier>();


        if (playerTeam.teamID == team.teamID)
        {
            backGround.SetActive(false);
            health.SetActive(false);
        }
        else
        {
            backGround.SetActive(true);
            health.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }

    public void Damaged()
    {
        health.transform.localScale = new Vector3(Remap(team.maxHealth, team.health), health.transform.localScale.y, health.transform.localScale.z);
    }

    float Remap(float max, float progress)
    {
        StopCoroutine("Fade");
        StartCoroutine("Fade");
        float scaledProgress = 0 + (progress - 0) * (1 - 0) / (max - 0);
        //               low2 + (value - low1) * (high2 - low2) / (high1 - low1)
        return scaledProgress;
    }

    IEnumerator Fade()
    {
        backGround.SetActive(true);
        health.SetActive(true);

        yield return new WaitForSeconds(5);

        backGround.SetActive(false);
        health.SetActive(false);
    }
}
