using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody projectileRb;
    public GameObject particle;
    public int damage = 10;



    private void Awake()
    {
        projectileRb = GetComponent<Rigidbody>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        float speed = 20.0f;
        projectileRb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        OutOfBounds();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        GameObject effectIns = Instantiate(particle, transform.position, transform.rotation);
        Destroy(effectIns, 1f);
        Destroy(gameObject);
        
        GameObject enemy = other.gameObject;
        enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
       
        
    }

 
    void OutOfBounds()
    {
        if(projectileRb.transform.position.x > 40)
        {
            Destroy(gameObject);
        }

        if (projectileRb.transform.position.z > 40)
        {
            Destroy(gameObject);
        }

        if (projectileRb.transform.position.z < -40)
        {
            Destroy(gameObject);
        }
        if (projectileRb.transform.position.x < -40)
        {
            Destroy(gameObject);
        }
    }


}
