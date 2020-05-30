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
        failureDialogues = Resources.LoadAll<AudioClip>(SceneManager.GetActiveScene().name + "/Failure/ScriptedCrash");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void playFailure(Vector3 point)
    {
        hasPlayedFailure = true;
        // Play scripted dialogue for crashes 1, 2, and last
        failureDialogues = Resources.LoadAll<AudioClip>(SceneManager.GetActiveScene().name + "/Failure/ScriptedCrash");
        if (TrackErrors.getCollisions() < failureDialogues.Length)
        {
            StartCoroutine(PlayError.PlayWarningClipCoroutine(failureDialogues[TrackErrors.getCollisions()]));
        }
        // Play generic crash dialogue for all other crashes
        else
        {
            StartCoroutine(PlayError.PlayWarningCoroutine(SceneManager.GetActiveScene().name + "/Failure/GenericCrash"));
        }
    }
}
