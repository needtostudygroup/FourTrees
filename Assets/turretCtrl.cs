using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretCtrl : MonoBehaviour
{
    private float updownspeed = 50f;
    private float turnspeed = 100f;
    private float TurnVerticalInputValue;
    private float TurnHorizontaIInputValue;
    float VerticalTurn = 0;
    float HorizontalTurn = 0;


    // Start is called before the first frame update
    void Start()
    {
        VerticalTurn = 0;
        HorizontalTurn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TurnVerticalInputValue = -Input.GetAxis("Mouse Y");
        TurnHorizontaIInputValue = Input.GetAxis("Mouse X");

        VerticalTurn = VerticalTurn + TurnVerticalInputValue * updownspeed * Time.deltaTime;
        HorizontalTurn = HorizontalTurn + TurnHorizontaIInputValue * turnspeed * Time.deltaTime;

        if (VerticalTurn <= 0 && VerticalTurn >= -80)
        {
            transform.rotation = Quaternion.Euler(VerticalTurn, HorizontalTurn, 0f);
        }
        else
        {
            if (VerticalTurn < -80)
            {
                transform.rotation = Quaternion.Euler(-80f, HorizontalTurn, 0f);
            }
            else if (VerticalTurn > 0)
            {
                transform.rotation = Quaternion.Euler(0f, HorizontalTurn, 0f);
            }

        }

    }

}

