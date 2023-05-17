using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 50f;
    public GameObject impactEffect;
    public int damage = 20;
   

    public void Seek (Transform _target)
    {
        target = _target;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (target == null)
        {
            Destroy(gameObject);
            return;
        }

       

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

        
    }

    private void HitTarget()
    {
        GameObject effectIns =Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns,1f);
        Destroy(gameObject);
        DamageEnemy(target);
        
        
    }

    private void DamageEnemy(Transform enemy)
    {
        EnemyHealth e = enemy.GetComponent<EnemyHealth>();
        e.TakeDamage(damage);
    }


}