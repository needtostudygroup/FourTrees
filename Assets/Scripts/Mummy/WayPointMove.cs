using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMove : MonoBehaviour
{
    //이동 속도
    public float speed = 1.0f;
    //회전 속도 조절 계수
    public float damping = 1.0f;    

    private Transform tr;
    private Transform[] points;
    private int nextIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        points = GameObject.Find("WayPointGroup").GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveWayPoint();
    }
    
    void MoveWayPoint() {
        //바라보는 벡터 계산
        Vector3 direction = points[nextIndex].position - tr.position;
        //산출된 벡터의 회전 각도 계산
        Quaternion rot = Quaternion.LookRotation(direction);
        //부드러운 회전 처리
        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
        //전진 방향으로 이동
        tr.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider coll)
    {
        //Point에 충돌 여부
        if (coll.tag == "WAY_POINT")
        {
            nextIndex++;
            if (nextIndex >= points.Length)
                Destroy(this.gameObject);
        }
    }
}
