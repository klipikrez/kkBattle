using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class gass : MonoBehaviour
{
    [SerializeField]

    public Vector3[] target = new Vector3[0];
    public GameObject[] wait = new GameObject[0];

    public bool isSelected = false;

    public NavMeshAgent navMeshAgent;

    public float stoppingDistance = 20;

    public Animator anim;
    bool once1 = true;
    bool once2 = true;
    void Awake()
    {


        isSelected = false;

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        
        if(navMeshAgent == null)
        {
            Debug.LogError("nema navMesh budi uradi bolje ovde:" + gameObject.name);
        }

    }

    void Update()
    {
        /*gleda da je dsao de treba da ide*/
        if (target.Length != 0)
        {
            if (anim != null && once1)
            {
                anim.SetBool("idle", false);
                once1 = false;
            }
            once2 = true;
            SetDestinationVector(target[0]);
            if ((Vector3.Distance(transform.position, target[0]) <= stoppingDistance) && wait[0] == null)
            {
                RemoveFirst();
            }
        }
        else
        {
            if (anim != null && once2)
            {
                anim.SetBool("idle", true);
                once2 = false;
            }
            once1 = true;
            navMeshAgent.isStopped = true;
        }


    }


    void SetDestinationVector(Vector3 targetVector)
    {
        //ovde je bilo siSelected

            navMeshAgent.isStopped = false;
            
            navMeshAgent.SetDestination(targetVector);
            target[0] = targetVector;

    }

    public void AddFirst(Vector3 goTo, GameObject isBuilding)
    {
        if (target.Length > 1)
        {
            //move everything up by one
            Array.Resize(ref target, target.Length + 1);
            Array.Resize(ref wait, wait.Length + 1);
            for (int i = target.Length-1; i > 0; i--)
            {
                wait[i] = wait[i - 1];
                target[i] = target[i - 1];

            }
            wait[0] = isBuilding;
            target[0] = goTo;
            //add at the first spot
        }
        else
        {
            Array.Resize(ref target, target.Length + 1);
            Array.Resize(ref wait, wait.Length + 1);
            wait[0] = isBuilding;
            target[0] = goTo;
        }
    }


    public void RemoveAll()
    {
        Array.Resize(ref wait,0 );
        Array.Resize(ref target, 0);
    }


    public void AddLast(Vector3 goTo, GameObject isBuilding)
    {
        
        Array.Resize(ref wait, wait.Length + 1);

        Array.Resize(ref target, target.Length + 1);
        target[target.Length - 1] = goTo;
        wait[wait.Length - 1] = isBuilding;
    }


    public void RemoveFirst()
    {
        if (target.Length > 1)
        {
            for (int i = 0; i < target.Length-1; i++)
            {
                wait[i] = wait[i + 1];
                target[i] = target[i + 1];

            }
            Array.Resize(ref target, target.Length - 1);
            Array.Resize(ref wait, wait.Length - 1);
        }
        else
        {
            RemoveAll();
        }

    }

    public void Selected()
    {
        isSelected = true;
       
    }

    public void Deselected()
    {
        isSelected = false;
        
    }
}
