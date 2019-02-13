using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMover : MonoBehaviour
{
    Rigidbody rb;

    public float maxSpeed;
    public float acce;

    [HideInInspector]
    public float speed = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speed += acce * Time.deltaTime;
        if (speed > maxSpeed)
        {
           speed = maxSpeed;
        }
        rb.velocity = transform.forward*speed;
    }


    public void kickback(bool spinning)
    {
        if (spinning)
        {
            speed -= 0.6f;
        }
        else
        {
            speed -= 2.5f;
        }

        if (speed < 0)
        {
            speed = 0;
        }
    }
}
