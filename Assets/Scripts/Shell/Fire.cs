using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    private float MIN_Power = 500.0f;
    private float MAX_Power = 3000.0f;
    public float currentPower;
    private float PushTime = 0.9f;
    private bool isPushed = false;
    public Transform firePosition;
    private float chargeSpeed;
    public Slider PowerGage;
    public Shell shell;

    // Start is called before the first frame update
    void Start()
    {
        currentPower = MIN_Power;
        PowerGage.maxValue = MAX_Power;
        PowerGage.value = currentPower;
        chargeSpeed = (MAX_Power - MIN_Power) / PushTime;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)//push space bar
        {
            shell.direction = this.transform.forward + new Vector3(0, 1.0f, 0);
            isPushed = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))//released spcae bar
        {
            if (currentPower >= MAX_Power)
                currentPower = MAX_Power;
            shell.power = currentPower;
            Instantiate(shell, firePosition.position, firePosition.rotation);
            isPushed = false;
            currentPower = MIN_Power;//reset
            shell.power = MIN_Power;//reset
        }
        if (isPushed)//space bar is being pushed
        {
            currentPower += chargeSpeed * Time.deltaTime;
            PowerGage.value = currentPower;
        }
    }
}
