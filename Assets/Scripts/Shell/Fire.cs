using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    private float MIN_POWER = 500.0f;
    private float MAX_POWER = 3000.0f;
    public float currentPower;
    private float PushTime = 0.9f;
    private bool isSpacePressed = false;
    public Transform firePosition;
    private float chargeSpeed;
    public Slider PowerGage;
    public Shell shell;

    // Start is called before the first frame update
    void Start()
    {
        currentPower = MIN_POWER;
        PowerGage.maxValue = MAX_POWER;
        PowerGage.value = currentPower;
        chargeSpeed = (MAX_POWER - MIN_POWER) / PushTime;
    }


    // Update is called once per frame
    void Update()
    {
        //push space bar
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            shell.direction = this.transform.forward + new Vector3(0, 1.0f, 0);
            isSpacePressed = true;
        }
        //released spcae bar
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (currentPower >= MAX_POWER)
                currentPower = MAX_POWER;
            shell.power = currentPower;
            Instantiate(shell, firePosition.position, firePosition.rotation);
            isSpacePressed = false;
            //reset
            currentPower = MIN_POWER;
            shell.power = MIN_POWER;
        }
        //space bar is being pressed
        if (isSpacePressed)
        {
            currentPower += chargeSpeed * Time.deltaTime;
            PowerGage.value = currentPower;
        }
    }
}
