using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    private static float maxTime = 600.0f;
    private static float currentTime { get; set; }

    private static bool isTracking = false;
    // Start is called before the first frame update
    void Start()
    {
        // waitTime = 100.0f;
        isTracking = false;
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTracking)
        {
            currentTime -= Time.deltaTime;
        }
    }

    //because currentTime is static, it needs a method to access
    public static float getCurrentTime()
    {
        return currentTime;
    }

    //because currentTime is static, it needs a method to access
    public static void resetTime()
    {
        //same as Start()
        isTracking = false;
        currentTime = maxTime;
    }
    public static float decrementTime(float seconds)
    {
        return currentTime + seconds;
    }
    public static void setTracking(bool enabled)
    {
        isTracking = enabled;
    }
    public static bool getTracking()
    {
        return isTracking;
    }
    public static float getProgress()
    {
        return currentTime / maxTime;
    }
}
