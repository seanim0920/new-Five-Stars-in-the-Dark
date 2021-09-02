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
    void Update()
    {
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
        if (Input.GetButtonDown("SteerLeft"))
        {
            controlFunctions.StartSteer(false);
        }
        if (Input.GetButtonUp("SteerLeft"))
        {
            controlFunctions.EndSteer(false);
        }
        if (Input.GetButtonDown("SteerRight"))
        {
            controlFunctions.StartSteer(true);
        }
        if (Input.GetButtonUp("SteerRight"))
        {
            controlFunctions.EndSteer(true);
        }
    }
}