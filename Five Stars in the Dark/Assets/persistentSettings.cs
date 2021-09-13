using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persistentSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SettingsManager.loadSettings();
    }

    void OnApplicationQuit()
    {
        SettingsManager.saveSettings();
    }

    void OnApplicationPause()
    {
        SettingsManager.saveSettings();
    }

    void OnApplicationFocus()
    {
        SettingsManager.saveSettings();
    }

    private void OnDisable()
    {
        SettingsManager.saveSettings();
    }

    private void OnDestroy()
    {
        SettingsManager.saveSettings();
    }
}
