using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    private float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            move(Vector3.forward * speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            move(Vector3.left * speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            move(Vector3.back * speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            move(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void move(Vector3 movePoint)
    {
        transform.Translate(movePoint);
    }
}
