﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckLosing : MonoBehaviour
{
    private bool lost = false;
    AudioSource soundSource;
    public AudioSource secondSoundSource;

    private void Start()
    {
        lost = false;
        soundSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!lost && CountdownTimer.getTracking() && (CountdownTimer.getCurrentTime() <= 0 || TrackErrors.getErrors() >= 5))
        {
            lost = true;
            CountdownTimer.setTracking(false);
            print("lose!");
            ScoreStorage.Instance.setScoreAll();
            MasterkeyFailScreen.currentLevel = SceneManager.GetActiveScene().name;
            StartCoroutine(failScreenSwitch());
        }
    }

    IEnumerator failScreenSwitch()
    {
        PlayerControls controls;
        controls = GameObject.Find("Player").GetComponent<PlayerControls>();
        controls.gameObject.tag = "Pedestrian";
        StartCoroutine(controls.shutOff());
        //if we stop it, carcollisions can restart the clip again after the collision is finished
        ConstructLevelFromMarkers.levelDialogue.volume = 0;
        yield return new WaitForSeconds(4);
        //pick a random soundsource from the set of failure dialogues for this level.
        AudioClip[] failClips = Resources.LoadAll<AudioClip>(SceneManager.GetActiveScene().name + "/Failure");
        if (failClips.Length > 0)
        {
            soundSource.clip = failClips[Random.Range(0, failClips.Length)];
            soundSource.Play();
            yield return new WaitWhile(() => soundSource.isPlaying);
        }

        yield return new WaitForSeconds(0.5f);
        secondSoundSource.Play();

        LoadScene.Loader("FailScreen");
        lost = false;
    }
}