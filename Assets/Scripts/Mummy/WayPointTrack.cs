using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointTrack : MonoBehaviour
{
    public Color lineColor = Color.black;
    private Transform[] points;

    private void OnDrawGizmos()
    {
        //라인의 색상 지정
        Gizmos.color = lineColor;
        //WayPointGroup의 모든 자식 point 추출
        points = GetComponentsInChildren<Transform>();

        int nextIndex = 1;
        //현재 가야 할 위치
        Vector3 currPos = points[nextIndex].position;
        //다음에 가야 할 위치
        Vector3 nextPos;

        //Point를 순회하면서 라인 그리기
        for (int i = 1; i < points.Length; i++) {
            //다음 위치 지정
            nextPos = points[nextIndex++].position;
            //시작 위치에서 종료 위치까지 라인을 그림
            Gizmos.DrawLine(currPos, nextPos);

            currPos = nextPos;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        
    }
}
