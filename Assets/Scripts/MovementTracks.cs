using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTracks : MonoBehaviour, TankMove.TankMoveHandler
{
    public Material drawMaterial;

    // Map 에 속한 tank들
    private TankMove[] tanks;
    private Material dentMaterial;
    private RenderTexture splatMap;

    // Map 크기
    private float mapWidth;
    private float mapHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        // Map 크기
        mapWidth = GetComponent<MeshRenderer>().bounds.size.x;
        mapHeight = GetComponent<MeshRenderer>().bounds.size.z;
        
        tanks = GetComponentsInChildren<TankMove>();
        
        for (int i = 0; i < tanks.Length; i++)
        {
            tanks[i].handler = this;
        }
        
        dentMaterial = GetComponent<MeshRenderer>().material;
        splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        dentMaterial.SetTexture("_Splat", splatMap);
    }

    /**
     * 탱크가 움직였음을 알리는 이벤트
     */
    public void OnTankMovement(Vector3 movePoint)
    {
        // 맵 크기에 따른 좌표 보정
        movePoint.x += mapWidth / 2;
        movePoint.z += mapHeight / 2;

        // Normalization
        movePoint.x /= mapWidth;
        movePoint.z /= mapHeight;
        
        // Map textureCoordinate와 상하좌우 대칭이라 이를 보정하기 위함
        movePoint = new Vector3(1,1,1) - movePoint;
        
        drawMaterial.SetVector("_Coordinate", new Vector4(movePoint.x, movePoint.z));
        
        RenderTexture temp =
            RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(splatMap, temp);
        Graphics.Blit(temp, splatMap, drawMaterial);
        RenderTexture.ReleaseTemporary(temp);
    }
}
