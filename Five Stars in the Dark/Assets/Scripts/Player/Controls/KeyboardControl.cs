using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControl : MonoBehaviour
{
    private PlayerControls controlFunctions;
    // Start is called before the first frame update
    void Start()
    {
        controlFunctions = GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!controlFunctions.enabled) return;

        if (Input.GetButtonDown("Accel"))
        {
            controlFunctions.AccelerateStart();
        }
        if (Input.GetButtonUp("Accel"))
        {
            controlFunctions.AccelerateEnd();
        }
        if (Input.GetButtonDown("Brake"))
        {
            controlFunctions.BrakeStart();
        }
        if (Input.GetButtonUp("Brake"))
        {
            controlFunctions.BrakeEnd();
        }
        if (Input.GetButton("SteerLeft"))
        {
            controlFunctions.SteerLeft();
        }
        if (Input.GetButton("SteerRight"))
        {
            controlFunctions.SteerRight();
        }
    }
}