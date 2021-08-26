using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkipButtonEnable : MonoBehaviour
{
    GameObject level;
    Text text;
    Button button;

    void Start()
    {
        level = GameObject.Find("LevelConstructor");
        text = GetComponentInChildren<Text>();
        button = GetComponent<Button>();
        GetComponent<Button>().onClick.AddListener(() => {
            StartCoroutine(level.GetComponent<SkipCutscenes>().skipCutsceneCoroutine());
            text.enabled = false;
        });

        if (PlaythroughManager.hasPlayedLevel(PlaythroughManager.currentLevelIndex))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!CountdownTimer.getTracking())
        {
            if (!button.enabled)
            {
                button.enabled = true;
                text.enabled = true;
            }
        }
        else
        {
            button.enabled = false;
            text.enabled = false;
        }

        if (button.enabled)
        {
            if (SettingsManager.toggles[2])
            {
                if (Gamepad.current.name.Contains("DualShock"))
                {
                    text.text = "SKIP\n(O)";
                }
                else
                {
                    text.text = "SKIP\n(B)";
                }
            }
            else
            {
                text.text = "SKIP\n(L)";
            }

            if (Input.GetKeyDown("l") || (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame))
            {
                StartCoroutine(level.GetComponent<SkipCutscenes>().skipCutsceneCoroutine());
                text.enabled = false;
            }
        }
    }
}
