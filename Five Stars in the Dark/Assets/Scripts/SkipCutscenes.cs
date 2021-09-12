using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SkipCutscenes : MonoBehaviour
{
    public static bool isSkipping = false; //could be altered in pause screen

    public AudioSource levelDialogue;
    public AudioSource skipStartSound;
    public AudioSource skipLoopSound;
    public AudioSource skipEndSound;

    public IEnumerator skipCutsceneCoroutine()
    {
        if (!CountdownTimer.getTracking() && !PauseMenu.isPaused)
        {
            OverlayStatic.turnOnStatic();
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
            OverlayStatic.turnOffStatic();
            //print(levelDialogue.time);
        }
    }
}
