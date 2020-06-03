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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void playFailure(Vector3 point)
    {
        GetComponent<NPCMovement>().enabled = false;
        StartCoroutine(PlayError.PlayWarningCoroutine("Stoplight"));
        //base.playFailure(point);
    }
}
