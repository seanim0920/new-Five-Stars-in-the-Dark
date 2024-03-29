﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackErrors : MonoBehaviour
{
    //set to public so it can be accessed from the end screen
    private static float errors { get; set; }
    private static int collisions { get; set; }

    public static void IncrementErrors(float fractionalError)
    {
        errors += fractionalError;
        collisions++;
        ScoreStorage.Instance.setScoreErrors(errors);
        //AudioSource.PlayClipAtPoint(errorSound, player.position);
    }
    public static void IncrementErrorsWithoutCollisions(float fractionalError)
    {
        errors += fractionalError;
        ScoreStorage.Instance.setScoreErrors(errors);
        //AudioSource.PlayClipAtPoint(errorSound, player.position);
    }

    public static void IncrementErrors()
    {
        errors++;
        collisions++;
        ScoreStorage.Instance.setScoreErrors(errors);
        //AudioSource.PlayClipAtPoint(errorSound, player.position);
    }
    // Start is called before the first frame update
    void Start()
    {
        errors = 0;
        collisions = 0;
    }

    public static void resetErrors()
    {
        //same as Start()
        errors = 0;
        collisions = 0;
    }

    //because errors is static, it needs a method to access
    public static float getErrors()
    {
        return errors;
    }

    //because errors is static, it needs a method to access
    public static int getCollisions()
    {
        return collisions;
    }
}