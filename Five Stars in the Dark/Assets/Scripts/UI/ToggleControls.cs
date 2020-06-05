using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleControls : MonoBehaviour
{
    public void ToggleSteeringWheel(bool toggleValue)
    {
        Debug.Log("Wheel: " + toggleValue);
        SettingsManager.toggles[0] = toggleValue;
        SettingsManager.setToggles();
    }

    public void ToggleKeyboard(bool toggleValue)
    {
        Debug.Log("Keyboard " + toggleValue);
        SettingsManager.toggles[1] = toggleValue;
        SettingsManager.setToggles();
    }

    public void ToggleGamepad(bool toggleValue)
    {
        Debug.Log("Gamepad " + toggleValue);
        SettingsManager.toggles[2] = toggleValue;
        SettingsManager.setToggles();
    }
}
