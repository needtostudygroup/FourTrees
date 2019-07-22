using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private Rigidbody rigidbody;
    
    public Vector3 direction;
    public float power;

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.AddForce(direction * power);
    }

    private void OnCollisionEnter(Collision other)
    {
        print("enter");
    }
}
