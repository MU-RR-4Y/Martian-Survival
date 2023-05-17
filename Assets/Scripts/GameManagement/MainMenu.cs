using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject instructions;
    public GameObject button;
    public bool onInstructionScreen = false;

    public void Start()
    {
        instructions.SetActive(false);
    }

    //public void Update()
    //{
    //    if (button.enabled)
    //    {
    //        instructions.SetActive(true);
    //    }
    //    else
    //    {
    //        instructions.SetActive(false);
    //    }
    //}

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}
