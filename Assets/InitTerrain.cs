using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitTerrain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RenderTexture cursorSplat = new RenderTexture(1024 * 10, 1024 * 10, 0, RenderTextureFormat.ARGBFloat);
        Material material = GetComponent<Terrain>().materialTemplate;
        material.SetTexture("_CursorSplat", cursorSplat);
    }
}
