using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    private const float MIN_POWER = 500.0f;
    private const float MAX_POWER = 3000.0f;
    public float CurrentPower;
    private const float PushTime = 0.9f;
    private bool IsSpacePressed = false;
    public Transform firePosition;
    private float chargeSpeed;
    public Slider PowerGage;
    public Shell shell;

    // Start is called before the first frame update
    void Start()
    {
        CurrentPower = MIN_POWER;
        PowerGage.maxValue = MAX_POWER;
        PowerGage.value = CurrentPower;
        chargeSpeed = (MAX_POWER - MIN_POWER) / PushTime;
    }


    // Update is called once per frame
    void Update()
    {
        //push space bar
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            shell.direction = this.transform.forward + new Vector3(0, 1.0f, 0);
            IsSpacePressed = true;
        }
        //released spcae bar
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (CurrentPower >= MAX_POWER)
                CurrentPower = MAX_POWER;
            shell.power = CurrentPower;
            Instantiate(shell, firePosition.position, firePosition.rotation);
            IsSpacePressed = false;
            //reset
            CurrentPower = MIN_POWER;
            shell.power = MIN_POWER;
        }
        //space bar is being pressed
        if (IsSpacePressed)
        {
            CurrentPower += chargeSpeed * Time.deltaTime;
            PowerGage.value = CurrentPower;
        }
    }
}
