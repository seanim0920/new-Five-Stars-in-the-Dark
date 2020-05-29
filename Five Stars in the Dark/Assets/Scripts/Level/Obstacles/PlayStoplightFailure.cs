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
        StartCoroutine(PlayError.PlayWarningCoroutine(SceneManager.GetActiveScene().name + "/Failure/Stoplight"));
        //base.playFailure(point);
    }
}
