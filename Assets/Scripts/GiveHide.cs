using UnityEngine;
using System.Collections;

public class GiveHide : MonoBehaviour
{
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
    }

    public void OnTriggerEnter()
    {
        Debug.Log("You can now hide");
        car.canHide = true;
        Destroy(gameObject);
    }
}
