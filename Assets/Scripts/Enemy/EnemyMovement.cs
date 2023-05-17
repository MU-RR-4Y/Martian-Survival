using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private NavMeshAgent enemy;
    public GameObject PlayerTarget;
    public Animator anim;
    private float distanceToTarget;
    private GameObject detectedTurret;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!detectedTurret)
        {
            target(PlayerTarget);
        }
        else
        {
            target(detectedTurret);
        }

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (detectedTurret)
            return;


        if (collision.gameObject.tag == "Turret")
        {
            detectedTurret = collision.gameObject;
        }
    }

    void target(GameObject target)
    {
        enemy.SetDestination(target.transform.position);
        distanceToTarget = Vector3.Distance(target.transform.position, enemy.transform.position);
        if(distanceToTarget <= enemy.stoppingDistance)
        {
            anim.SetBool("CanAttack", true);
        }
        else
        {
            anim.SetBool("CanAttack", false);
        }
        
    }
}
