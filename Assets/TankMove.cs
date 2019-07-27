using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    private float speed = 1.0f;
    public TankMoveHandler handler;
    

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
        handler.OnTankMovement(transform.position);
    }

    /**
     * 탱크가 움직였음을 알림
     * Map에 탱크의 경로에 따라 지형을 바꾸는 기능에 사용
     */
    public interface TankMoveHandler
    {
        void OnTankMovement(Vector3 movePoint);
    }
}
