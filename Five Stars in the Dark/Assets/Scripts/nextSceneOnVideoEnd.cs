using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextSceneOnVideoEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public static UnityEngine.Video.VideoPlayer videoPlayer;
    private ConstructLevelFromMarkers levelScript;
    private AudioSource dialogue;

    public AudioSource playSfx;
    public AudioSource pauseSfx;
    public PauseMenu pauseScript;
    public GameObject rewindText;
    public GameObject canvas;

    private void Awake()
    {
        UnityEngine.Video.VideoPlayer videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.Prepare();
        videoPlayer.loopPointReached += MovieFinished;

        levelScript = GameObject.Find("LevelConstructor").GetComponent<ConstructLevelFromMarkers>();
        levelScript.enabled = false;
        dialogue = GameObject.Find("LevelConstructor").GetComponent<AudioSource>();

        StartCoroutine(rewindEffectThenPlayCoroutine());
    }

    IEnumerator rewindEffectThenPlayCoroutine()
    {
        rewindText.SetActive(true);
        //pauseScript.enabled = false; //menu is old and I dont have time to replace it
        pauseSfx.Play();
        OverlayStatic.overlaid = true;
        dialogue.pitch = -7;
        dialogue.timeSamples = dialogue.clip.samples - 2;
        dialogue.Play();
        while (dialogue.isPlaying)
        {
            OverlayStatic.overlaid = true;
            yield return new WaitForSeconds(0);
        }
        rewindText.SetActive(false);
        playSfx.Play();
        OverlayStatic.overlaid = false;
        //pauseScript.enabled = true;
        yield return new WaitForSeconds(1);
        dialogue.timeSamples = 0;
        levelScript.enabled = true;
        dialogue.pitch = 1;
        dialogue.Play();
        GetComponent<UnityEngine.Video.VideoPlayer>().Play();
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
