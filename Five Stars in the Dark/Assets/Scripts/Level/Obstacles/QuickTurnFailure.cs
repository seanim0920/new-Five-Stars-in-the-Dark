using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickTurnFailure : ObstacleFailure
{
    private static int numFaliures = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        failureDialogues = Resources.LoadAll<AudioClip>("LevelFiles/" + SceneManager.GetActiveScene().name + "/Failure/QuickTurn");
        // numFaliures = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void playFailure(Vector3 point)
    {
        // Play failure dialogue sequentially
        if(numFaliures >= failureDialogues.Length)
        {
            // dialogueSource.clip = failureDialogues[numFaliures];
            // numFaliures++;
            numFaliures = 0;
        }
        Debug.Log("What # dialogue? " + numFaliures);
        // StartCoroutine(PlayError.PlayWarningClipCoroutine(failureDialogues[numFaliures]));
        dialogueSource.clip = failureDialogues[numFaliures];
        base.playFailure(point);
        numFaliures++;
    }
}
