using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 endPos;
    public float speed = 1.0f;

    private bool moving = false;
    private bool opening = true;
    private Vector3 startPos;
    private float delay = 0.0f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (moving)
        {
            if (opening)
            {
                MoveDoor(endPos);
            }
            else
            {
                MoveDoor(startPos);
            }
        }
    }

    void MoveDoor(Vector3 goalPos)
    {
        float dist = Vector3.Distance(transform.position, goalPos);

        if (dist > 0.1f)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                goalPos,
                speed * Time.deltaTime
            );
        }
        else
        {
            transform.position = goalPos;

            if (opening)
            {
                delay += Time.deltaTime;

                if (delay > 1.5f)
                {
                    opening = false;
                    delay = 0;
                }
            }
            else
            {
                moving = false;
                opening = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>() != null)
        {
            moving = true;
            opening = true;
            delay = 0;
        }
    }
}