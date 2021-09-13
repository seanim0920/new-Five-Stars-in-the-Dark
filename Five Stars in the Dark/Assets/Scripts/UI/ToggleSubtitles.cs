using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSubtitles : MonoBehaviour
{
    public void OnOffSubtitles()
    {
        if(SettingsManager.toggles[3])
        {
            SettingsManager.toggles[3] = false;

        }
        else
        {
            SettingsManager.toggles[3] = true;
        }
    }
}
