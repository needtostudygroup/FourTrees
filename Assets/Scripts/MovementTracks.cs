using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTracks : MonoBehaviour
{
    // Map 크기
    private float mapWidth;
    private float mapHeight;

    // Start is called before the first frame update
    void Start()
    {
        // Map 크기
        mapWidth = GetComponent<MeshRenderer>().bounds.size.x;
        mapHeight = GetComponent<MeshRenderer>().bounds.size.z;
    }

    private Vector3 correctGlobalVectorToMapLocationVector(Vector3 vec)
    {
        // 맵 크기에 따른 좌표 보정
        vec.x += mapWidth / 2;
        vec.z += mapHeight / 2;

        // Normalization
        vec.x /= mapWidth;
        vec.z /= mapHeight;
        
        // Map textureCoordinate와 상하좌우 대칭이라 이를 보정하기 위함
        vec = new Vector3(1,1,1) - vec;
        return vec;
    }
}
