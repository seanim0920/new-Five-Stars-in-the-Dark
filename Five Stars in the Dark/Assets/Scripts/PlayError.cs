using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayError : MonoBehaviour
{
    public static AudioSource source;
    public static bool playingHurtSound = false;
    private static List<string> CompletedDialogues = new List<string>();
    private static string[] oofs = {"Ah!", "Urgh!", "Ooh!", "Argh!", "Hrngh!", "Ahh!", "Aah!", "Oof!", "Oogh!", "Ugh!", "Egh!", "Mmh!" };

    private static string playingError = "";
    private static bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        resetCompletedDialogues();
        source = GetComponent<AudioSource>();
    }

    public static void resetCompletedDialogues()
    {
        CompletedDialogues = new List<string>();
        playingHurtSound = false;
        playingError = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (playingError != "")
        {
            StartCoroutine(PlayOofThenWarningCoroutine(playingError));
            playingError = "";
        }
        if (source != null && ConstructLevelFromMarkers.levelDialogue != null && CountdownTimer.getTracking())
            source.panStereo = ConstructLevelFromMarkers.levelDialogue.panStereo;
    }

    public static void PlayOofThenWarning(string obstacleType)
    {
        playingError = obstacleType;
    }

    private IEnumerator PlayOofThenWarningCoroutine(string obstacleType)
    {
        StartCoroutine(PlayWarningCoroutine("Oofs", false));
        yield return new WaitForSeconds(2);
        StartCoroutine(PlayWarningCoroutine(obstacleType, true));
        playingError = "";
    }

    public static IEnumerator PlayWarningCoroutine(string obstacleType, bool markPlayed)
    {
        AudioClip[] defaultHurtSounds = Resources.LoadAll<AudioClip>("LevelFiles/" + SceneManager.GetActiveScene().name + "/Situational/Default");
        AudioClip defaultHurtSound = defaultHurtSounds[Random.Range(0, defaultHurtSounds.Length)];
        // ^ incase no sounds are found in the directory

        AudioClip passengerHurtSound = Resources.Load<AudioClip>("LevelFiles/" + SceneManager.GetActiveScene().name + "/Situational/" + obstacleType);

        if (passengerHurtSound == null)
        {
            //save a local copy of situational dialogues
            List<AudioClip> situationalDialogues = new List<AudioClip>(Resources.LoadAll<AudioClip>("LevelFiles/" + SceneManager.GetActiveScene().name + "/Situational/" + obstacleType));
            if (situationalDialogues.Count <= 0)
            {
                Debug.Log("no hurt sounds found, empty or nonexistent directory!");
                passengerHurtSound = defaultHurtSound;
            }
            else
            {
                Debug.Log("Looking for oofs.");
                // Pick random failure dialogue
                System.Random rand = new System.Random();
                passengerHurtSound = situationalDialogues[Random.Range(0, situationalDialogues.Count)];

                // Re-pick if this sound was played before and remove this sound from local copy
                bool allUsed = false;
                while (CompletedDialogues.Contains(passengerHurtSound.name))
                {
                    situationalDialogues.Remove(passengerHurtSound);
                    if (situationalDialogues.Count <= 0)
                    {
                        Debug.Log("all dialogues in this folder were played!");
                        passengerHurtSound = defaultHurtSound;
                        
                        if (CompletedDialogues.Contains(passengerHurtSound.name))
                        {
                            yield break; //teeeeemppppppppp
                        }

                        allUsed = true;
                        break;
                    }
                    else
                    {
                        passengerHurtSound = situationalDialogues[Random.Range(0, situationalDialogues.Count)];
                    }
                }
                if (markPlayed) //without checking allused, it will also mark the default oofs as completed
                {
                    Debug.Log("will not play this again!");
                    CompletedDialogues.Add(passengerHurtSound.name);
                }
            }
        }

        AudioSource dialogue = ConstructLevelFromMarkers.levelDialogue;
        bool wasPlaying = dialogue.isPlaying;
        int currentTimePosition = CalculatePauseTime(dialogue, passengerHurtSound);

        if (string.Equals(obstacleType, "Oofs"))
        {
            subtitleText.changeSubtitle(oofs[Random.Range(0, oofs.Length)], passengerHurtSound.length + 1);
        }
        else
        {
            subtitleText.changeSubtitle("[Passenger Disappointment]", passengerHurtSound.length + 1);
        }

        yield return new WaitForSeconds(passengerHurtSound.length + 1f);
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
        isPlaying = true;
        AudioSource dialogue = ConstructLevelFromMarkers.levelDialogue;
        bool wasPlaying = dialogue.isPlaying;
        int currentTimePosition = CalculatePauseTime(dialogue, passengerHurtSound);

        //wait for... idk 3 seconds?
        yield return new WaitForSeconds(passengerHurtSound.length + 1f);  //note: should play an "oof" first before the angry passenger dialogue
        //resume dialogue
        playingHurtSound = false;
        dialogue.timeSamples = currentTimePosition;
        if (wasPlaying) //also check isplaying
            dialogue.Play();
        Debug.Log("Resuming Dialogue");
    }

    private static int CalculatePauseTime(AudioSource dialogue, AudioClip passengerHurtSound)
    {
        //for oofs, just use a random set of subs
        //StartCoroutine(subtitleText.changeSubtitleCoroutine("oof", passengerHurtSound.length));

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
