using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * 1.0f);
    }
}
