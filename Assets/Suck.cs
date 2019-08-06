using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suck : MonoBehaviour
{
    public Texture2D cursorTexture;

    private Material suckMouseMaterial;
    private Camera camera;
    private Ray ray;
    private RaycastHit hit;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponentInChildren<Camera>();
        suckMouseMaterial = new Material(Shader.Find("Unlit/SuckMouseShader"));
        suckMouseMaterial.SetTexture("_CursorTexture", cursorTexture);
        suckMouseMaterial.SetFloat("_CursorSize", 100);
        suckMouseMaterial.SetColor("_Color", Color.yellow);
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

            Debug.Log(hit.textureCoord);
            suckMouseMaterial.SetVector("_Coordinate", hit.textureCoord);
            Material material = terrain.materialTemplate;
            RenderTexture cursorSplat = (RenderTexture) material.GetTexture("_CursorSplat");
            RenderTexture temp = RenderTexture.GetTemporary(cursorSplat.width, cursorSplat.height, 0, cursorSplat.format);
            Graphics.Blit(cursorSplat, temp);
            Graphics.Blit(temp, cursorSplat, suckMouseMaterial);
            RenderTexture.ReleaseTemporary(temp);
        }
    }
}
