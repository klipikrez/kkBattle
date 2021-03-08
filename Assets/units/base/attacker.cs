using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class attacker : MonoBehaviour
{

    enemyDictionary dictionary;

    public AudioSource source;


    /*
    LineRenderer lr;
    public GameObject lineStart;
    */
    public VisualEffect bullet;
    public GameObject bulletRotate;
    public float bulletSpeed = 100;

    public Animator anim;
    public Quaternion gunOfsetRot;
    public GameObject gun;
    public bool lockRotation = true;
    public int rot = 0;

    public float damage;
    float canAttack;
    public float AttackRate = 2f;
    float attackRate;
    public bool canAttackFlying = false;
    public GameObject currentlyEngaging;

    public Quaternion resetRotation;

    bool once1 = false;
    bool once2 = false;

    void Start()
    {
       
        attackRate = Random.Range(AttackRate * 0.5f, AttackRate * 1.5f);

        dictionary = gameObject.GetComponent<enemyDictionary>();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentlyEngaging != null)
        {

            if (once1)
            {
                once1 = false;
                if (anim != null)
                {
                    anim.SetBool("attacking", true);
                }
            }
            once2 = true;
            canAttack += Time.deltaTime;



            Quaternion lookAt = Quaternion.LookRotation((currentlyEngaging.transform.position - gun.transform.position).normalized);


            if (lockRotation)
            {
                switch (rot)
                {
                    case 0:
                        gun.transform.rotation = new Quaternion(lookAt.x, resetRotation.y, resetRotation.z, lookAt.w) * gunOfsetRot;
                        break;
                    case 1:
                        gun.transform.rotation = new Quaternion(resetRotation.x, lookAt.y, resetRotation.z, lookAt.w) * gunOfsetRot;
                        break;
                    case 2:
                        gun.transform.rotation = new Quaternion(resetRotation.x, resetRotation.y, lookAt.z, lookAt.w) * gunOfsetRot;
                        break;
                    default:
                        break;
                }


            }
            else
            {
                gun.transform.rotation = lookAt * gunOfsetRot;
            }

            if (canAttack > attackRate)
            {
                fire();
            }

        }
        else
        {
            if (once2)
            {
                once2 = false;
                if (anim != null)
                {
                    anim.SetBool("attacking", false);
                }
            }
            once1 = true;
            if (dictionary.IDs.Length > 1)
            {

                dictionary.RemoveFirst();
                updateCurrentEnemy();
            }

            canAttack = 0;

            gun.transform.localRotation = resetRotation;
        }

    }


    void fire()
    {
        if (bullet != null)
        {

            if (anim != null)
            {
                anim.Play("attack");
            }


            //rotate particle emitter towards currentlyEngaging
            Quaternion lookAt2 = Quaternion.LookRotation((currentlyEngaging.transform.position - bulletRotate.gameObject.transform.position).normalized);
            bulletRotate.gameObject.transform.rotation = lookAt2;

            bullet.SetFloat("life", Vector3.Distance(currentlyEngaging.transform.position, bullet.gameObject.transform.position) / bulletSpeed);
            bullet.SetVector3("velocity", new Vector3(0, 0, bulletSpeed));


            bullet.SendEvent("fire");
        }
        StartCoroutine("TakeDamage", currentlyEngaging);
        //currentlyEngaging.GetComponent<teamIdentefier>().TakeDamage(damage);
        if (source != null)
        {
            source.Play(0);
        }
        canAttack = 0f;
    }

    IEnumerator TakeDamage(GameObject enemy)
    {
        yield return new WaitForSeconds(Vector3.Distance(enemy.transform.position, bullet.gameObject.transform.position) / bulletSpeed);

        if (enemy != null)
        {
            enemy.GetComponent<teamIdentefier>().TakeDamage(damage);
        }

    }


    public void updateCurrentEnemy()
    {

        //ako imamo nesto u spisku napadaca. napadaj
        if (dictionary.enemies.Count != 0)
        {
            if (dictionary.enemies[dictionary.IDs[0]] != null)
            {
                currentlyEngaging = dictionary.enemies[dictionary.IDs[0]];
            }
            else
            {
                dictionary.RemoveFirst();
                currentlyEngaging = dictionary.enemies[dictionary.IDs[0]];
            }
        }
        else
        {
            currentlyEngaging = null;
        }


    }
}
