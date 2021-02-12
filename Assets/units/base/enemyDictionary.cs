using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();
    public int[] IDs = new int[1];
    int i = 0;
    public void addEnemies(GameObject go)
    {
        if (IDs.Length > i)
        {
            int id = go.GetInstanceID();
            
            if (!enemies.ContainsKey(id))
            {
                enemies.Add(id, go);


                Array.Resize(ref IDs, IDs.Length + 1);
                IDs[i] = id;
                
                i++;
            }
        }
    }


    public void clearEnemies()
    {
       

        enemies.Clear();

        Array.Resize(ref IDs, 1);
        IDs[0] = 0;
        i = 0;

    }


    void RemoveAt<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
        // finally, let's decrement Array's size by one
        Array.Resize(ref arr, arr.Length - 1);
        i--;
    }

    public void clearEnemy(GameObject go)
    {
        if (enemies.Count <= 1)
        {
            clearEnemies();
        }
        else
        {
            //is sotono HAHAHHAHhahAHAHHAHaz
            int id = go.GetInstanceID();
            enemies.Remove(id);
            i--;

            int ID = Array.IndexOf(IDs, id);

            RemoveAt(ref IDs, ID);
        }
    }

    public void RemoveFirst()
    {
        
        i--;
        int id = IDs[0];
        enemies.Remove(id);

        int ID = 0;

        while (ID < IDs.Length - 1)
        {
            IDs[ID] = IDs[ID + 1];

            ID++;
        }
        Array.Resize(ref IDs, IDs.Length - 1);
    }
}