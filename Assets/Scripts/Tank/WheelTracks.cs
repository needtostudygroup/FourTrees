using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTracks : MonoBehaviour
{
    // 스키드마크 크기
    [UnityEngine.Range(1, 5000)]
    public float size;
    
    // 스키드마크 굵기
    [UnityEngine.Range(0, 1)]
    public float strength;
    
    // 이외 스키드마크 관련 Material 세팅
    public Shader shader;
    private RenderTexture splatMap;
    private Material mapMaterial;
    private Material drawMaterial;
    
    public GameObject map;
    private RaycastHit groundHit;
    
    // Start is called before the first frame update
    void Start()
    {
        splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        mapMaterial = map.GetComponent<Terrain>().materialTemplate;
        mapMaterial.SetTexture("_Splat", splatMap);
        drawMaterial = new Material(shader);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 움직이고 있을 때
        // TODO: WASD말고 아예 움직이고 있다는 flag를 두고 체크하는게 좋을것같음 
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Vector3 right = transform.right * 0.5f;
        
            // 왼쪽 바퀴가 바닥에 닿는지
            if (Physics.Raycast(transform.position - right, -transform.up, out groundHit, 1f))
            {
                drawSkidmark(groundHit);
            }
        
            // 오른쪽 바퀴가 바닥에 닿는지
            if (Physics.Raycast(transform.position + right, -transform.up, out groundHit, 1f))
            {
                drawSkidmark(groundHit);
            }
        }
    }
    
    // 스키드마크 그리기
    private void drawSkidmark(RaycastHit hit)
    {
        drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y));
        drawMaterial.SetFloat("_Strength", strength);
        drawMaterial.SetFloat("_Size", size);
        
        RenderTexture temp =
            RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(splatMap, temp);
        Graphics.Blit(temp, splatMap, drawMaterial);
        RenderTexture.ReleaseTemporary(temp);
    }
}
