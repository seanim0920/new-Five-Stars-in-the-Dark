using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkipMovies : MonoBehaviour
{
    public static bool isSkipping = false;

    public GameObject rewindText;
    public AudioSource skipStartSound;
    public AudioSource skipLoopSound;
    public AudioSource skipEndSound;
    private UnityEngine.Video.VideoPlayer videoPlayer;
    private AudioSource dialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogue = GameObject.Find("LevelConstructor").GetComponent<AudioSource>();
        isSkipping = false;
        if (GetComponent<UnityEngine.Video.VideoPlayer>())
        {
            videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!rewindText.activeSelf && Input.GetKeyDown("l") || (Gamepad.current != null && Gamepad.current.buttonNorth.isPressed))
        {
            if (isSkipping)
                isSkipping = false;
            else
                StartCoroutine(skipIntro());
        }
    }

    private IEnumerator skipIntro()
    {
        if (!PauseMenu.isPaused)
        {
            OverlayStatic.turnOnStatic();
            isSkipping = true;
            skipStartSound.Play();
            skipLoopSound.Play();
            dialogue.pitch = 10;
            videoPlayer.playbackSpeed = 10;
            while (videoPlayer.isPlaying && isSkipping)
            {
                yield return new WaitForSeconds(0);
            }
            dialogue.pitch = 1;
            videoPlayer.playbackSpeed = 1;
            skipEndSound.Play();
            skipLoopSound.Stop();
            isSkipping = false;
            OverlayStatic.turnOffStatic();
        }
    }
}
