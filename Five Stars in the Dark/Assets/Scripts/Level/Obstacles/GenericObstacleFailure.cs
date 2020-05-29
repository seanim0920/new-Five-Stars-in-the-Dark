using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericObstacleFailure : ObstacleFailure
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
        StartCoroutine(PlayError.PlayWarningCoroutine(SceneManager.GetActiveScene().name + "/Failure/GenericCrash"));
        //base.playFailure(point);
    }
}