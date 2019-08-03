using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretCtrl : MonoBehaviour
{
    private float updownspeed = 50f;
    private float turnspeed = 100f;
    private float turnVerticalInputValue;
    private float turnHorizontaIInputValue;
    float verticalTurn = 0;
    float horizontalTurn = 0;


    // Start is called before the first frame update
    void Start()
    {
        verticalTurn = 0;
        horizontalTurn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        turnVerticalInputValue = -Input.GetAxis("Mouse Y");
        turnHorizontaIInputValue = Input.GetAxis("Mouse X");

        verticalTurn = verticalTurn + turnVerticalInputValue * updownspeed * Time.deltaTime;
        horizontalTurn = horizontalTurn + turnHorizontaIInputValue * turnspeed * Time.deltaTime;


        if (verticalTurn < -80)
        {
            transform.rotation = Quaternion.Euler(-80f, horizontalTurn, 0f);
        }
        else if (verticalTurn > 0)
        {
            transform.rotation = Quaternion.Euler(0f, horizontalTurn, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(verticalTurn, horizontalTurn, 0f);
        }
    }

}

