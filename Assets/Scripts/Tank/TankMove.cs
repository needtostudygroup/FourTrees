using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float Speed = 12f;
    public float TurnSpeed = 180f;

    private string MovementAxisName;
    private string TurnAxisName;

    private Rigidbody Rigidbody;

    private float MovementInputValue;
    private float TurnInputValue;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.isKinematic = true;

        MovementAxisName = "Vertical";
        TurnAxisName = "Horizontal";

    }

    void Update()
    {
        MovementInputValue = Input.GetAxis(MovementAxisName);
        TurnInputValue = Input.GetAxis(TurnAxisName);

        Vector3 movement = transform.forward * MovementInputValue * Speed * Time.deltaTime;

        //rigidbody를 옮겨준다
        Rigidbody.MovePosition(Rigidbody.position + movement);

        float turn = TurnInputValue * TurnSpeed * Time.deltaTime;

        //회전시켜줘
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        //rigidbody에도 적용시커줘
        Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);
    }


}
