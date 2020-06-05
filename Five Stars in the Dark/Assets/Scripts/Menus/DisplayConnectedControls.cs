using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DisplayConnectedControls : MonoBehaviour
{
    [SerializeField]
    private Toggle questionableWheel;
    [SerializeField]
    private Toggle questionableGamepad;
    void OnEnable()
    {
        questionableWheel.gameObject.SetActive(SteeringWheelInput.checkConnected());
        questionableGamepad.gameObject.SetActive(Gamepad.current != null);
    }
}
