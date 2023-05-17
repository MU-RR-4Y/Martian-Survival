using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turret : MonoBehaviour
{
    public Transform target;

    [Header("Attributes")]
    public float range = 15.0f;
    public float fireRate = 2.0f;
    private float fireCountdown = 0f;

    [Header("UnitySetUpFields")]
    public Transform partToRotate;
    public float turnSpeed = 10.0f;
    public GameObject bulletPrefab;
    public Transform[] firepoints;

    public int MaxHealth = 60;
    public int currentHealth;
    public bool isDestroyed;

    private AudioSource playerAudio;
    public AudioClip shootSound;
    public AudioClip targetSound;
    public float targetAudioRate;

    //public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        // turret checks for nearest target and rechecks every half a sec
        currentHealth = MaxHealth;
        //healthBar.SetMaxHealth(MaxHealth);
        InvokeRepeating("UpdateTarget", 0, 0.5f);
        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        //float offset = (target.gameObject.transform.localScale.y) / 2;

        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
       

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;

        checkHealth();
    }

    private void Shoot()

    {
        int randomIndex = Random.Range(0, firepoints.Length-1);
        Transform firePoint = firepoints[randomIndex];
        GameObject bulletGameObj = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        playerAudio.PlayOneShot(shootSound);
        bullet bullet = bulletGameObj.GetComponent<bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }





    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // create list of enemies using 'enemy' tag.
        GameObject[] BossEnemies = GameObject.FindGameObjectsWithTag("Boss");
        List<GameObject> targets = new List<GameObject>();
        foreach(GameObject BossEnemy in BossEnemies)
        {
            targets.Add(BossEnemy);
        }
        foreach (GameObject enemy in enemies)
        {
            targets.Add(enemy);
        }
        float shortestDistance = Mathf.Infinity; // default is infinty so that distance is not set when we dont have any enemys
        GameObject nearestEnemy = null;


        foreach (GameObject enemy in targets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);// check distance between turrent on enemy
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            //playerAudio.PlayOneShot(targetSound);
            playTargetAudio();
        }
        else
        {
            target = null;
        }

    }

    private void playTargetAudio()
    {
        targetAudioRate = Random.Range(0, 20);
        if (targetAudioRate == 1)
        {
            playerAudio.PlayOneShot(targetSound);
        }
    }



    // creates visual display of range on turret
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void takeDamage(int amount)
    {
        currentHealth -= amount;
        //healthBar.SetHealth(currentHealth);
    }

    public void checkHealth()
    {
        if (currentHealth <= 0)
        {
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
