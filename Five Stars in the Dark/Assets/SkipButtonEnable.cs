using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkipButtonEnable : MonoBehaviour
{
    GameObject level;
    Vector3 lastMouseCoordinate = Vector3.zero;
    float lastTimeMoved = 3;
    Animator anim;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        level = GameObject.Find("LevelConstructor");
        anim = GetComponent<Animator>();
        text = GetComponentInChildren<Text>();

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
        if (!anim.IsInTransition(0))
        {
            if (!CountdownTimer.getTracking())
            {
                if (!gameObject.activeSelf)
                    gameObject.SetActive(true);
                anim.CrossFade("PauseUp", 0.6f);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        if (gameObject.activeSelf)
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
                StartCoroutine(level.GetComponent<SkipCutscenes>().skipIntroCoroutine());
                text.enabled = false;
            }
        }
    }
}
