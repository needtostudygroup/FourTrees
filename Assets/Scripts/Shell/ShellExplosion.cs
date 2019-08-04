using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellExplosion : MonoBehaviour
{
    public Texture paintTexture;
    public Shader paintableShader;
    private Material paintableMaterial;
    private Material hitObjectMaterial;
    private RenderTexture hitObjectSplat;

    private RaycastHit hit;
    
    // Start is called before the first frame update
    void Start()
    {
        paintableMaterial = new Material(paintableShader);
        paintableMaterial.SetColor("_Color", Color.green);
        paintableMaterial.SetTexture("_PaintTexture", paintTexture);
    }

    private void OnCollisionEnter(Collision other)
    {
        // HitableObject 를 만나면 오브젝트 사라짐
        if (other.collider.CompareTag(GlobalVariable.HitableObject) && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            hitObjectMaterial = other.collider.gameObject.GetComponent<MeshRenderer>().material;
            hitObjectSplat = (RenderTexture) hitObjectMaterial.GetTexture("_PaintSplat");
            if (hitObjectSplat == null)
            {
                hitObjectSplat = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
                hitObjectMaterial.SetTexture("_PaintSplat", hitObjectSplat);
            }

            Debug.Log(hit.textureCoord);
            paintableMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y));

            RenderTexture temp = RenderTexture.GetTemporary(hitObjectSplat.width, hitObjectSplat.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(hitObjectSplat, temp);
            Graphics.Blit(temp, hitObjectSplat, paintableMaterial);
            
            RenderTexture.ReleaseTemporary(temp);
        }
    }
}
