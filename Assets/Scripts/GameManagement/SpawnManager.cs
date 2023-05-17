using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    //GameObject[] EnemyList = []
    public GameObject baseEnemy;
    public GameObject powerfulEnemy;
    public GameObject powerUp;

    public GameObject[] spawnPoints;




    //Wave Syststem Setup
    [System.Serializable] //allows us to change instances of class inside inspector
    public class Wave
    {
        public int number;
        public int numberofEnemies;
        public int enemiesRemaining;

        public void removeEnemy()
        {
            enemiesRemaining -= 1;
        }

    }
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemiesText;
    public Wave _wave;
    public bool canSpawn = true;
    public int numberofWaves = 10;
    public float timeBetweenWaves = 10.0f;

    public bool GameWin ;

   




    private void Awake()
    {
        _wave.number = 1;
        _wave.numberofEnemies = 15;
        _wave.enemiesRemaining = _wave.numberofEnemies;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemiesText.text = "Enemies Remaining: " + _wave.enemiesRemaining;
        waveText.text = "Round: " + _wave.number;
        SpawnWave();


    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = "Round: " + _wave.number;
        enemiesText.text = "Enemies Remaining: " + _wave.enemiesRemaining;
        MangeWaves();
        winGame();
        
    }

    void MangeWaves()
    {
        if(_wave.enemiesRemaining == 0)
        {
            winGame();
            if (canSpawn)
            {
            _wave.numberofEnemies += 15;
            _wave.enemiesRemaining = _wave.numberofEnemies;
            _wave.number += 1;
            SpawnWave();
            }
        }
    }

    void winGame()
    {
        if(_wave.number == numberofWaves && _wave.enemiesRemaining == 0)
        {
            // action win method, to be setup on GameManager
            Debug.Log("Player wins");
            canSpawn = false;
            Invoke("goToWinScreen",0f);
            

        }

    }

    public void goToWinScreen()
    {
        SceneManager.LoadScene("EndGame",LoadSceneMode.Single);
        Cursor.lockState = CursorLockMode.None;
    }

    void SpawnWave()
    {
        if (canSpawn)
        {
            Invoke("SpawnBoss", 5.0f);
            for (int j = 0; j < _wave.numberofEnemies - 1; j++)
            {
                Invoke("SpawnEnemy", 1.5f);
            }
        }
    }


    //SpawnEnemy function
    void SpawnEnemy()
    {

        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 spawnPos = spawnPoint.transform.position;

        Instantiate(baseEnemy, spawnPos, baseEnemy.transform.rotation);

    }

    void SpawnBoss()
    {
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 spawnPos = spawnPoint.transform.position;
       Instantiate(powerfulEnemy, spawnPos, powerfulEnemy.transform.rotation);
    }






}
