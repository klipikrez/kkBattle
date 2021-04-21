using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.multiplayerOverride;

public class constructionMain : NetworkBehaviour
{
    public GameObject prefab;
    //public GameObject mat;

    public Renderer rend;
    public float maxProgress = 100f;
    public float currentProgress = 0f;
    public float speed = 1f;
    public GameObject progressBar;

    public int builders = 0;

    Collider[] goBuilders;
    public float checkRadious = 10f;
    public LayerMask layers;

    public Transform goHere;

    private networkManagerOverride room;
    public networkManagerOverride Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as networkManagerOverride;
        }
    }

    void Start()
    {
        //rend = mat.GetComponent<Renderer>();
        //rend.material.shader = Shader.Find("construction");//Gets the Shader
    }

    void Update()
    {
        //Debug.Log(builders);
        countDown();   
        if(currentProgress>maxProgress)
        {
            placePrefab();
        }
        CheckBuilders();
        UpdateProgressBar(maxProgress, currentProgress);
    }

    void UpdateProgressBar(float time, float progress)
    {
        float scaledProgress = 0 + (progress - 0) * (1 - 0) / (time - 0);
        //               low2 + (value - low1) * (high2 - low2) / (high1 - low1)
        float finalValue = scaledProgress * scaledProgress;
        
        progressBar.transform.localScale = new Vector3(finalValue, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
    }

    void countDown()
    {
        currentProgress += Time.deltaTime * (builders* speed);
        float setProgress = currentProgress / 100f;
        rend.material.SetFloat("progress", setProgress);
    }



    void CheckBuilders()
    {
        goBuilders = Physics.OverlapSphere(transform.position, checkRadious, layers);
        //Debug.Log(goBuilders.Length);
        builders = goBuilders.Length;
    }

    [Command]
    void placePrefab()
    {
        GameObject radiViseAjdeeee = Instantiate(prefab, transform.position, transform.rotation);
        radiViseAjdeeee.GetComponent<teamIdentefier>().teamID = gameObject.GetComponent<teamIdentefier>().teamID;
        radiViseAjdeeee.GetComponent<teamIdentefier>().teamColor = gameObject.GetComponent<teamIdentefier>().teamColor;
        //deleteObjs();
        NetworkServer.Spawn(radiViseAjdeeee, Room.GamePlayers[gameObject.GetComponent<teamIdentefier>().teamID].gameObject);
        delete();
    }
    /*
    [Command]
    void deleteObjs()
    {

    }*/

    public void delete()
    {
        if (goBuilders != null)
        {
            foreach (Collider builder in goBuilders)
            {
                builder.gameObject.GetComponent<gass>().RemoveFirst();
            }
        }
        Destroy(gameObject);
    }
    
}

///////////ok znaci imas go od konstrukcije u rts set mozda isto sphere cast za check????? nzm