using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suck : MonoBehaviour
{
    public Texture2D cursorTexture;
    
    private Camera camera;
    private Ray ray;
    private RaycastHit hit;
    
    // Start is called before the first frame update
    void Start()
    {
//        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
//            Debug.Log(hit.point);
        }
    }
}
