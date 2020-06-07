using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialCarFailure : ObstacleFailure
{
    public bool hasPlayedFailure = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        failureDialogues = Resources.LoadAll<AudioClip>("LevelFiles/" + SceneManager.GetActiveScene().name + "/Failure/ScriptedCrash");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void playFailure(Vector3 point)
    {
        // Play scripted dialogue for crashes 1, 2, 3, and last
        if (TrackErrors.getCollisions() < 5)
        {
            failureDialogues = Resources.LoadAll<AudioClip>("LevelFiles/" + SceneManager.GetActiveScene().name + "/Failure/ScriptedCrash");
            if (TrackErrors.getCollisions() < failureDialogues.Length)
            {
                StartCoroutine(PlayError.PlayWarningClipCoroutine(failureDialogues[TrackErrors.getCollisions() - 1]));
            }
            else
            {
                StartCoroutine(PlayError.PlayWarningClipCoroutine(failureDialogues[failureDialogues.Length - 1]));
            }

        }
        // Play generic crash dialogue for all other crashes
        else
        {
            StartCoroutine(PlayError.PlayWarningCoroutine("GenericCrash"));
            Debug.Log(failureDialogues[numDialogue]);
        }
        hasPlayedFailure = true;
    }
}
