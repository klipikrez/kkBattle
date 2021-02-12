using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class attackerDetection : MonoBehaviour
{

    attacker attack;
    teamIdentefier team;
    public teamIdentefier selfTeamID;
    public enemyDictionary dictionary;

    // Start is called before the first frame update
    void Start()
    {
        attack = gameObject.transform.parent.gameObject.GetComponent<attacker>();

        if (selfTeamID == null)
        {
            selfTeamID = gameObject.transform.parent.gameObject.GetComponent<teamIdentefier>();
        }

        if (dictionary == null)
        {
            dictionary = gameObject.transform.parent.gameObject.GetComponent<enemyDictionary>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<teamIdentefier>() != null)
        {           
            team = other.gameObject.GetComponent<teamIdentefier>();

            if (team.teamID != selfTeamID.teamID)
            {

                if (attack.canAttackFlying)
                {

                    //dictionary.clearEnemies();
                    dictionary.addEnemies(other.gameObject);
                    attack.updateCurrentEnemy();

                }
                else
                {
                    if (!team.isFlying)
                    {
                        
                        //ako nisu na istom timu NAPAD

                        //dictionary.clearEnemies();
                        dictionary.addEnemies(other.gameObject);
                        attack.updateCurrentEnemy();


                    }
                }
            }
            team = null;
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<teamIdentefier>() != null)
        {
            team = other.gameObject.GetComponent<teamIdentefier>();
            //ako nisu na istom timu NAPAD
            if (team.teamID != selfTeamID.teamID)
            {
                dictionary.clearEnemy(other.gameObject);
                attack.updateCurrentEnemy();
            }
            
        }
        
    }

}
