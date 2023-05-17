using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerScript : MonoBehaviour
{
    public int MaxHealth = 200;
    public int currentHealth;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI resourcesText;
    public int resources;
    public int towerCost = 50;
    public bool canBuild;
    public static event Action OnPlayerDeath;

    public HealthBar healthBar;
    public bool hasPowerUp;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        //healthText.text = "Health: " + currentHealth;
        healthBar.SetMaxHealth(MaxHealth);
        resources = 50;
        resourcesText.text = "Resources: " + resources;
        checkResources();
        
    }

    // Update is called once per frame
    void Update()
    {
        //healthText.text = "Health: " + currentHealth;
        healthBar.SetHealth(currentHealth);
        resourcesText.text = "Resources: " + resources;
        checkResources();
        heal();
    }

    


    public void checkResources()
    {
        if(resources >= towerCost)
        {
            canBuild = true;
        }
        else
        {
            canBuild = false;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnPlayerDeath?.Invoke();
        }
    }

    public void addResources(int amount)
    {
        resources += amount;
    }

    public void removeResources(int amount)
    {
        resources -= amount;
    }

    public void payForTower()
    {
        removeResources(towerCost);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
        }
    }

    private void heal()
    {
        if (hasPowerUp)
        {
            currentHealth = MaxHealth;
            hasPowerUp = false;
        }
    }

}
