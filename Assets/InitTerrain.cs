using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitTerrain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RenderTexture cursorSplat = new RenderTexture(1024 * 8, 1024 * 8, 0, RenderTextureFormat.ARGBFloat);
        Debug.Log(cursorSplat.antiAliasing);
        RenderTexture terrainChangedMap = new RenderTexture(1024 * 4, 1024 * 4, 0, RenderTextureFormat.RFloat);
        Material material = GetComponent<Terrain>().materialTemplate;
        
        material.SetTexture("_CursorSplat", cursorSplat);
        material.SetTexture("_TerrainChangedMap", terrainChangedMap);
    }
}
