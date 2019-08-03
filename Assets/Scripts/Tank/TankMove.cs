using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float speed = 12f;
    public float turnSpeed = 180f;

    private string movementAxisName;
    private string turnAxisName;

    private Rigidbody rigidbody;

    private float movementInputValue;
    private float turnInputValue;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        movementAxisName = "Vertical";
        turnAxisName = "Horizontal";

    }

    void Update()
    {
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);

        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;

        //rigidbody를 옮겨준다
        rigidbody.MovePosition(rigidbody.position + movement);

        float turn = turnInputValue * turnSpeed * Time.deltaTime;

        //회전시켜줘
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        //rigidbody에도 적용시커줘
        rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
    }


}
