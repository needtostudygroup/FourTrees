using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private Rigidbody rigidbody;
    public AudioClip collisionSound;

    // Shell을 Tracking 하는 카메라
    private Camera camera;
    
    // Shell 발포 방향
    public Vector3 direction;
    
    // Shell 발포 세기
    public float power;

    private void Start()
    {
        // Shell 은 렌더링과 동시에 주어진 direction과 power 로 발사됨
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(direction * power);

        camera = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        // 포탄 시점으로 카메라 전환
        camera.transform.LookAt(rigidbody.velocity * 100);
    }

    private void OnCollisionEnter(Collision other)
    {
        // HitableObject 를 만나면 오브젝트 사라짐
        if (other.collider.tag == GlobalVariable.HitableObject)
        {
            AudioSource.PlayClipAtPoint(collisionSound, this.transform.position);
            Destroy(gameObject);
        }
    }
}
