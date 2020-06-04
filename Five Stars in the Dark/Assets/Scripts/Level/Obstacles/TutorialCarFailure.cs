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
        // Play scripted dialogue for crashes 1, 2, and last
        if (TrackErrors.getErrors() < 2 || TrackErrors.getErrors() > 7)
        {
            failureDialogues = Resources.LoadAll<AudioClip>("LevelFiles/" + SceneManager.GetActiveScene().name + "/Failure/ScriptedCrash");
            if (TrackErrors.getErrors() < failureDialogues.Length)
            {
                StartCoroutine(PlayError.PlayWarningClipCoroutine(failureDialogues[TrackErrors.getCollisions()]));
            }
            else
            {
                StartCoroutine(PlayError.PlayWarningClipCoroutine(failureDialogues[0]));
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
