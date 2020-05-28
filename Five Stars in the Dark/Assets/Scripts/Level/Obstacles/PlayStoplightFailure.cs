using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayStoplightFailure : ObstacleFailure
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        failureDialogues = Resources.LoadAll<AudioClip>(SceneManager.GetActiveScene().name + "/Failure/Stoplight");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void playFailure(Vector3 point)
    {
        // Play failure dialogue sequentially
        System.Random rand = new System.Random();
        numDialogue = rand.Next(0, failureDialogues.Length);
        Debug.Log("error made playing numDialogue: " + numDialogue);
        if (failureDialogues.Length > 0)
            StartCoroutine(PlayError.PauseDialogueCoroutine(failureDialogues[numDialogue]));
        //base.playFailure(point);
    }
}
