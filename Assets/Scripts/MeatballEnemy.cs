using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MeatballEnemy : MonoBehaviour
{
    public List<GameObject> meatballDestinations;
    Vector3 meatballStart;

    public float meatballSpeed = 1.0f;
    public float meatballWaitTime = 1.0f;

    public bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       meatballStart = GetComponent<Transform>().position;
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MoveMeatball();
        }
    }

    public void MoveMeatball()
    {
        StartCoroutine(MoveMeatballCoroutine());
    }

    public IEnumerator MoveMeatballCoroutine()
    {
        foreach (GameObject destination in meatballDestinations)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = destination.transform.position;
            float journeyLength = Vector3.Distance(startPosition, endPosition);
            float startTime = Time.time;
            while (Vector3.Distance(transform.position, endPosition) > 0.1f)
            {
                float distCovered = (Time.time - startTime) * meatballSpeed;
                float fractionOfJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
                yield return null;
            }
            transform.position = endPosition; // Ensure the final position is set
            yield return new WaitForSeconds(meatballWaitTime);
        }
       //Return to the starting position after reaching all destinations
        //transform.position = meatballStart;

    }
   
}
