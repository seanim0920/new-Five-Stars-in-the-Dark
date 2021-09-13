using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

// This class keeps track of the settings throughout each scene
public static class SettingsManager
{
    // Music, SFX, Dialogue, Radio + Audio Cues
    public static float[] volumes = {1f, 1f, 1f, 1f};
    // Steering Wheel, Keyboard, Gamepad
    public static bool[] toggles = {SteeringWheelInput.checkConnected(), false, Gamepad.current != null, true};

    public static void setToggles()
    {
        // Give priority to steering wheel controls
        if(toggles[0])
        {
            toggles[1] = false;
            toggles[2] = false;
        }
        // Then Gamepad controls
        else if(toggles[2])
        {
            toggles[1] = false;
        }

        string stringToggles = "";
        foreach (bool toggle in toggles)
        {
            stringToggles += (toggle) ? "toggled; " : "not toggled; ";
        }
        //Debug.Log(stringToggles);
        // Otherwise only keyboard controls are
        // toggled and we don't need to do anything
    }

    public static void saveSettings()
    {
        string path = Application.persistentDataPath + "/Settings.txt";
        string settings = "";
        foreach (float volume in volumes)
        {
            settings += volume + "-";
        }
        settings += "\n";
        foreach (bool toggle in toggles)
        {
            settings += toggle + "-";
        }
        //Debug.Log("levels as text: " + textLevels);
        File.WriteAllText(path, settings);
        //Debug.Log("Played this level? " + (playedLevels.Contains(levelBuildIndex) ? "y" : "n"));
    }

    public static void loadSettings()
    {
        //load settings
        string path = Application.persistentDataPath + "/Settings.txt";
        if (!File.Exists(path))
        {
            return;
        }

        // Else read from file and copy contents into temp list
        string savedSettings = File.ReadAllText(path);
        string[] volumesandtoggles = savedSettings.Split('\n');
        if (volumesandtoggles.Length <= 1) return;
        string[] savedVolumes = volumesandtoggles[0].Split('-');
        string[] savedToggles = volumesandtoggles[1].Split('-');
        for (int i = 0; i < savedVolumes.Length-1; i++) //we need to exclude the last character because it's blank
        {
            volumes[i] = float.Parse(savedVolumes[i]);
        }
        for (int i = 0; i < savedToggles.Length-1; i++)
        {
            toggles[i] = bool.Parse(savedToggles[i]);
        }
    }
}
