using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Shell shell;
    public Transform firePosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shell.direction = this.transform.forward + new Vector3(0, 1.0f, 0);
            shell.power = 2500.0f;
            Instantiate(shell, firePosition.transform.position, firePosition.transform.rotation);
        }
    }
}
