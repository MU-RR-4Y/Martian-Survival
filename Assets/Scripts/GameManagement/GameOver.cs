using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverMenu;
    public AudioSource audioSource;

    void Start()
    {
        gameOverMenu.SetActive(false);
    }

    public void OnEnable()
    {
        PlayerScript.OnPlayerDeath += EnableGameOverMenu;
    }

    public void OnDisable()
    {
        PlayerScript.OnPlayerDeath -= EnableGameOverMenu;
    }


    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        audioSource.Stop();
    }


    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
