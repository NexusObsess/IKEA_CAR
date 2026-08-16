using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    //int HPScore;
    int Time = 0;
    float speedDisplayHighScore = 0;

    CarController car;


    void Start()
    {
        car = FindFirstObjectByType<CarController>(); // Find car game objects by searching for the carController script

        if (car != null)
        {
            Debug.Log("Found GameObject: " + car.gameObject.name);
        }
        else
        {
            Debug.LogWarning("No GameObjects with CarController found.");
        }

        car.UIStats.text = "Health: " + car.HP + "\nSpeed: " + car.speedDisplay + "\nTime: " + Time + " seconds";
        InvokeRepeating("TimeUp", 0.5f, 1f); // increasing time display every second
    }

    void Update()
    {
        car.UIStats.text = "Health: " + car.HP + "\nSpeed: " + car.speedDisplay + "\nTime: " + Time + " seconds"; // updates the UI display every frame

        if (car.speedDisplay > speedDisplayHighScore)
        {
            speedDisplayHighScore = car.speedDisplay;
        }
    }

    void TimeUp()
    {
        Time ++;
    }
}
