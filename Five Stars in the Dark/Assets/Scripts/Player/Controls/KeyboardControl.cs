﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControl : MonoBehaviour
{
    private PlayerControls controlFunctions;
    private float accelAmount = 0;
    private float breakAmount = 0;
    private float strafeAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        controlFunctions = GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Input.GetKey("up") || Input.GetKey("down")))
        {
            if (Input.GetKey("up"))
            {
                controlFunctions.speedUp(accelAmount);
            }
            if (Input.GetKey("down"))
            {
                controlFunctions.slowDown(breakAmount);
                strafeAmount *= 0.92f;
            }
        } else
        {
            controlFunctions.returnToNeutralSpeed();
        }

        strafeAmount *= 0.97f;
        accelAmount *= 0.97f;
        // breakAmount *= 0.97f;
        controlFunctions.strafe(strafeAmount); //2.08f normalizes strafeamount
        if (!controlFunctions.enabled) strafeAmount = 0;

        if (Input.GetKey("up") && accelAmount < 0.98f)
        {
            accelAmount += 0.02f;
        }
        if (Input.GetKey("down") && breakAmount < 0.98f)
        {
            // breakAmount += 0.02f;
            breakAmount = 0.02f;
        }
        if (Input.GetKey("left") || Input.GetKey("right"))
        {
            if (Input.GetKey("right"))
            {
                strafeAmount += 0.01f;
            }
            else
            {
                strafeAmount -= 0.01f;
            }
        } else if (!controlFunctions.enabled)
        {
            strafeAmount = 0;
        }
    }
}