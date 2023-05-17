using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    GameObject player;
    PlayerScript playerScript;

    GameObject turret;
    Turret turretscript;


    public int damage;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();

        turret = GameObject.FindGameObjectWithTag("Turret");
        turretscript = turret.GetComponent<Turret>();
    }

    // Start is called before the first frame update
    void Start()
    {
        damage = 25;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        {
            attackPlayer(playerScript);
        }

        if(other.tag == "Turret") {
            attackTurret(turretscript);
        }

    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        Debug.Log("Still Attacking");
    //    }
    //}

    

    void attackPlayer(PlayerScript playerScript)
    {
        playerScript.TakeDamage(damage);
    }

    void attackTurret(Turret turretscript)
    {
        turretscript.takeDamage(damage);
    }
}
