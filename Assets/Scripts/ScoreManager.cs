using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    //int HPScore;
    int Time = 0;
    int FinalHealth = 0;
    int TimeTaken = 0;
    float speedDisplayHighScore = 0;

    CarController car;

    ResultReciever results;
    //TextMeshProUGUI results;

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

        results = FindFirstObjectByType<ResultReciever>();

        if (results != null)
        {
            Results();
        }
        else
        {
            Debug.LogWarning("boo");
        }

        car.UIStats.text = "Health: " + car.HP + "\nSpeed: " + car.speedDisplay + "\nTime: " + Time + " seconds";
        InvokeRepeating("TimeUp", 0.5f, 1f); // increasing time display every second
    }

    void Update()
    {
        if (car != null)
        {
            UIStats();
        }

        Results();
    }

    void UIStats()
    {
        car.UIStats.text = "Health: " + car.HP + "\nSpeed: " + car.speedDisplay + "\nTime: " + Time + " seconds"; // updates the UI display every frame

        FinalHealth = car.HP;
        TimeTaken = Time;

        if (car.speedDisplay <= speedDisplayHighScore)
        {
            speedDisplayHighScore = car.speedDisplay;
        }
    }

    void TimeUp()
    {
        Time ++;
    }

    void Results()
    {
        results.results.text = "Health: " + FinalHealth + "\nMax Speed: " + car.speedDisplay + "\nTime Taken: " + Time + " seconds";
    }
}
