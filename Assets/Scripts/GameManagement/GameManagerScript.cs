using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    //Resources
    public int Points;
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        Points = 0;
        scoreText.text = "Score: " + Points;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + Points;
    }

    

    public void addPoints(int amount)
    {
        Points += amount;
    }

}
