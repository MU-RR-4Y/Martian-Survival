using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    public int currentHealth;

    GameObject player;
    PlayerScript playerScript;


    GameObject GameManager;
    GameManagerScript gameManagerScript;

    GameObject SpawnManager;
    SpawnManager spawnManager;

    public Animator anim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();

        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = GameManager.GetComponent<GameManagerScript>();

        SpawnManager = GameObject.FindGameObjectWithTag("SpawnManager");
        spawnManager = SpawnManager.GetComponent<SpawnManager>();

        anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (gameObject.CompareTag("Boss"))
            {
            anim.SetBool("IsDead", true);
            playerScript.addResources(30);
            gameManagerScript.addPoints(30);
            spawnManager._wave.removeEnemy();
            Instantiate(spawnManager.powerUp, gameObject.transform.position, spawnManager.powerUp.transform.rotation);
            Destroy(gameObject);
            

            }

            if (gameObject.CompareTag("Enemy"))
            {
                anim.SetBool("IsDead", true);
                playerScript.addResources(10);
                gameManagerScript.addPoints(10);
                spawnManager._wave.removeEnemy();
                Destroy(gameObject);
                

            }

        }
    }
}
