using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextSceneOnVideoEnd : MonoBehaviour
{
    // Start is called before the first frame update
    private UnityEngine.Video.VideoPlayer videoPlayer;
    private ConstructLevelFromMarkers levelScript;
    private AudioSource dialogue;
    private AudioSource reverseDialogue;

    public AudioSource playSfx;
    public AudioSource pauseSfx;
    public PauseMenu pauseScript;
    public GameObject rewindText;
    public GameObject canvas;

    private void Awake()
    {
        videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.Prepare();
        videoPlayer.loopPointReached += MovieFinished;

        levelScript = GameObject.Find("LevelConstructor").GetComponent<ConstructLevelFromMarkers>();
        levelScript.enabled = false;
        dialogue = GameObject.Find("LevelConstructor").GetComponent<AudioSource>();
        reverseDialogue = GetComponent<AudioSource>();

        StartCoroutine(rewindEffectThenPlayCoroutine());
    }

    IEnumerator rewindEffectThenPlayCoroutine()
    {
        rewindText.SetActive(true);
        //pauseScript.enabled = false; //menu is old and I dont have time to replace it
        pauseSfx.Play();
        OverlayStatic.turnOnStatic();
        reverseDialogue.Play();
        while (reverseDialogue.isPlaying)
        {
            videoPlayer.time = (reverseDialogue.clip.length - reverseDialogue.time) * 5;
            yield return new WaitForSeconds(0);
        }
        rewindText.SetActive(false);
        playSfx.Play();
        OverlayStatic.turnOffStatic();
        //pauseScript.enabled = true;
        yield return new WaitForSeconds(1);
        dialogue.timeSamples = 0;
        levelScript.enabled = true;
        dialogue.pitch = 1;
        dialogue.Play();
        videoPlayer.Play();
    }

    void MovieFinished(UnityEngine.Video.VideoPlayer vp)
    {
        StartCoroutine(waitASecThenLoadLevelCoroutine());
    }

    IEnumerator waitASecThenLoadLevelCoroutine()
    {
        playSfx.Play();
        GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
        yield return new WaitForSeconds(1);
        LoadScene.LoadNextScene();
    }
}
