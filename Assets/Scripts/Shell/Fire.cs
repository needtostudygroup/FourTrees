using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    private const float MIN_POWER = 500.0f;
    private const float MAX_POWER = 3000.0f;
    private const int MAX_BULLET = 10;
    public float currentPower;
    private int sign = 1;
    private const float PUSH_TIME = 0.9f;
    private bool IsSpacePressed = false;
    public AudioSource shootingAudio;
    public AudioClip shootingSound;
    public AudioClip failedSound;
    public Transform firePosition;
    private float chargeSpeed;
    public Slider powerGage;
    public Slider bulletGage;
    public Text BulletText;
    public int bulletCount;
    public Shell shell;

    // Start is called before the first frame update
    void Start()
    {
        bulletCount = 5;
        currentPower = MIN_POWER;
        powerGage.maxValue = MAX_POWER;
        powerGage.value = currentPower;
        BulletText.text = "x " + bulletCount;
        chargeSpeed = (MAX_POWER - MIN_POWER) / PUSH_TIME;
    }


    // Update is called once per frame
    void Update()
    {
        //push space bar
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            shell.direction = firePosition.transform.forward;
            if(bulletCount > 0)
            {
                IsSpacePressed = true;
            }
            else if (bulletCount <= 0)
            {
                IsSpacePressed = false;
                shootingAudio.clip = failedSound;
                shootingAudio.Play();
                currentPower = MIN_POWER;
                powerGage.value = currentPower;
            }
        }
        //released spcae bar
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if(bulletCount > 0)
            {
                bulletCount--;
                BulletText.text = "x " + bulletCount;
                shell.power = currentPower;
                Instantiate(shell, firePosition.position, firePosition.rotation);
                shootingAudio.clip = shootingSound;
                shootingAudio.Play();
                IsSpacePressed = false;
                //reset
                currentPower = MIN_POWER;
                shell.power = MIN_POWER;
            }
        }
        //땅파는키를 F라고 설정 했을 때
        if (Input.GetKeyDown(KeyCode.F) == true)
        {
            bulletGage.value += 1;
            if(bulletGage.value >= MAX_BULLET)
            {
                bulletCount++;
                BulletText.text = "x " + bulletCount;
                bulletGage.value = 0;
            }
        }
        
        //space bar is being pressed
        if (IsSpacePressed)
        {

            currentPower += chargeSpeed * Time.deltaTime * sign;
            powerGage.value = currentPower;

            if (currentPower >= MAX_POWER)
            {
                sign = -1;
                currentPower += chargeSpeed * Time.deltaTime * sign;
                powerGage.value = currentPower;
            }
            else if (currentPower <= MIN_POWER)
            {
                sign = 1;
            }
        }
    }
}
