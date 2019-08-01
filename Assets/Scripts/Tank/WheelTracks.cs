using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTracks : MonoBehaviour
{
    [UnityEngine.Range(1, 1000)]
    public float size;
    
    [UnityEngine.Range(0, 1)]
    public float strength;
    
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
        mapMaterial = map.GetComponent<MeshRenderer>().material;
        mapMaterial.SetTexture("_Splat", splatMap);
        drawMaterial = new Material(shader);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Vector3 right = transform.right * 0.5f;
        
            if (Physics.Raycast(transform.position - right, -transform.up, out groundHit, 1f))
            {
                drawLine(groundHit);
            }
        
            if (Physics.Raycast(transform.position + right, -transform.up, out groundHit, 1f))
            {
                drawLine(groundHit);
            }
        }
    }
    
    private void drawLine(RaycastHit hit)
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
