using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonText : MonoBehaviour
{
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "PAUSE\n(" + 
            ((SettingsManager.toggles[2]) ? "START"
            : "ESC") + ")";
    }
}
