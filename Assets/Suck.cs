using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Suck : MonoBehaviour
{
    public Texture2D cursorTexture;

    private int cursorSize = 100;
    private Material suckMouseMaterial;
    private Material suckMaterial;
    private Camera camera;
    private Ray ray;
    private RaycastHit hit;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponentInChildren<Camera>();
        suckMouseMaterial = new Material(Shader.Find("Unlit/SuckMouseShader"));
        suckMaterial = new Material(Shader.Find("Unlit/SuckShader"));
        
        suckMouseMaterial.SetTexture("_CursorTexture", cursorTexture);
        suckMouseMaterial.SetFloat("_CursorSize", cursorSize);
        suckMouseMaterial.SetColor("_Color", Color.yellow);
        
        suckMaterial.SetFloat("_CursorSize", cursorSize);
    }

    // Update is called once per frame
    void Update()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Terrain terrain = hit.collider.gameObject.GetComponent<Terrain>();
            if (terrain == null)
                return;

            // 마우스 커서 바꾸는 기능 (지형 변경 커서)
            suckMouseMaterial.SetVector("_Coordinate", hit.textureCoord);
            Material material = terrain.materialTemplate;
            RenderTexture cursorSplat = (RenderTexture) material.GetTexture("_CursorSplat");
            RenderTexture temp = RenderTexture.GetTemporary(cursorSplat.width, cursorSplat.height, 0, cursorSplat.format);
            Graphics.Blit(cursorSplat, temp);
            Graphics.Blit(temp, cursorSplat, suckMouseMaterial);
            RenderTexture.ReleaseTemporary(temp);
            
            // 마우스 좌클릭
            if (Input.GetMouseButton(0))
            {
                
            }
            
            // 마우스 우클릭
            else if (Input.GetMouseButton(1))
            {
                suckMaterial.SetVector("_Coordinate", hit.textureCoord);
                RenderTexture terrainChangedMap = (RenderTexture) material.GetTexture("_TerrainChangedMap");
                temp = RenderTexture.GetTemporary(terrainChangedMap.width, terrainChangedMap.height, 0, terrainChangedMap.format);
                Graphics.Blit(terrainChangedMap, temp);
                Graphics.Blit(temp, terrainChangedMap, suckMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
    }
}
