using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayError : MonoBehaviour
{
    public static AudioSource source;
    public static bool playingHurtSound = false;
    private static subtitleText subScript;
    // Start is called before the first frame update
    void Start()
    {
        subScript = GameObject.Find("SubtitleText").GetComponent<subtitleText>();
        playingHurtSound = false;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (source != null && ConstructLevelFromMarkers.levelDialogue != null && CountdownTimer.getTracking())
            source.panStereo = ConstructLevelFromMarkers.levelDialogue.panStereo;
    }
    public static IEnumerator PlayWarningCoroutine(string obstacleType)
    {
        AudioClip passengerHurtSound = Resources.Load<AudioClip>("LevelFiles/" + SceneManager.GetActiveScene().name + "/Failure/" + obstacleType);

        if (passengerHurtSound == null)
        {
            AudioClip[] failureDialogues = Resources.LoadAll<AudioClip>("LevelFiles/" + SceneManager.GetActiveScene().name + "/Failure/" + obstacleType);

            // Play random failure dialogue
            System.Random rand = new System.Random();
            int numDialogue = rand.Next(0, failureDialogues.Length);
            if (failureDialogues.Length <= 0) yield break;
            passengerHurtSound = failureDialogues[numDialogue];
        }

        AudioSource dialogue = ConstructLevelFromMarkers.levelDialogue;
        bool wasPlaying = dialogue.isPlaying;
        int currentTimePosition = CalculatePauseTime(dialogue, passengerHurtSound);

        //wait for... idk 3 seconds?
        yield return new WaitForSeconds(passengerHurtSound.length + 1f);  //note: should play an "oof" first before the angry passenger dialogue
        //resume dialogue
        playingHurtSound = false;
        dialogue.timeSamples = currentTimePosition;
        if (wasPlaying)
            dialogue.Play();
        Debug.Log("Resuming Dialogue");
    }

    //coroutines can't be called from static methods so it looks like copy-pasting will have to do for now.
    //hazard though, the two should be identical cause they almost do the same thing.
    public static IEnumerator PlayWarningClipCoroutine(AudioClip passengerHurtSound)
    {
        AudioSource dialogue = ConstructLevelFromMarkers.levelDialogue;
        bool wasPlaying = dialogue.isPlaying;
        int currentTimePosition = CalculatePauseTime(dialogue, passengerHurtSound);

        //wait for... idk 3 seconds?
        yield return new WaitForSeconds(passengerHurtSound.length + 1f);  //note: should play an "oof" first before the angry passenger dialogue
        //resume dialogue
        playingHurtSound = false;
        dialogue.timeSamples = currentTimePosition;
        if (wasPlaying)
            dialogue.Play();
        Debug.Log("Resuming Dialogue");
    }

    private static int CalculatePauseTime(AudioSource dialogue, AudioClip passengerHurtSound)
    {
        //for oofs, just use a random set of subs
        StartCoroutine(subtitleText.changeSubtitleCoroutine("oof", passengerHurtSound.length));

        source.clip = passengerHurtSound;
        source.Play();

        playingHurtSound = true;

        //find the last silent section of audio and rewind to it, or if not just rewind back a set amount of time
        float maxRewindTime = 2; //in seconds
        int maxRewindTimeInSamples = (int)((int)(dialogue.clip.samples / dialogue.clip.length) * maxRewindTime);
        float[] samples = new float[maxRewindTimeInSamples * dialogue.clip.channels]; //array to be filled with samples from the audioclip

        int currentTimePosition = dialogue.timeSamples - maxRewindTimeInSamples; //by default
        dialogue.clip.GetData(samples, currentTimePosition);

        dialogue.Pause();

        int foundSilences = 0;
        for (int i = samples.Length; i-- > 0;)
        {
            if (Mathf.Abs(samples[i]) == 0f)
            {
                foundSilences++;
                if (foundSilences >= 2)
                {
                    print("found silences.");
                    currentTimePosition += (i / dialogue.clip.channels);
                    break;
                }
            }
            else
            {
                foundSilences = 0;
            }
        }

        return currentTimePosition;
    }
}
