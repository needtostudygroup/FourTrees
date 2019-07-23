using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Camera camera;
    
    public Vector3 direction;
    public float power;

    // Shell 은 렌더링과 동시에 주어진 direction과 power 로 발사된됨
    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.AddForce(direction * power);

        camera = this.GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        camera.transform.LookAt(rigidbody.velocity);
    }

    private void OnCollisionEnter(Collision other)
    {
        // HitableObject 를 만나면 일어나는 반은
        if (other.collider.tag == GlobalVariable.HitableObject)
        {
            Destroy(gameObject);
        }
    }
}
