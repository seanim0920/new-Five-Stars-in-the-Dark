﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SkipCutscenes : MonoBehaviour
{
    public Text textToDisable;
    public static bool isSkipping = false;

    public AudioSource levelDialogue;
    public AudioSource skipStartSound;
    public AudioSource skipLoopSound;
    public AudioSource skipEndSound;
    // Start is called before the first frame update
    void Start()
    {
        isSkipping = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator skipIntroCoroutine()
    {
        if (!CountdownTimer.getTracking() && !PauseMenu.isPaused)
        {
            OverlayStatic.overlaid = true;
            isSkipping = true;
            skipStartSound.Play();
            skipLoopSound.Play();
            levelDialogue.pitch = 50;
            while (!CountdownTimer.getTracking() && isSkipping)
            {
                yield return new WaitForSeconds(0);
            }
            levelDialogue.pitch = 1;
            skipEndSound.Play();
            skipLoopSound.Stop();
            isSkipping = false;
            OverlayStatic.overlaid = false;
            //print(levelDialogue.time);
        }
    }
}
