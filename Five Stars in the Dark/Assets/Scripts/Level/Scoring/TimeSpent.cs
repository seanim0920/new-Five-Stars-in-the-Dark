using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeSpent : MonoBehaviour
{
    private static Text time;
	public static float remainder = 120f;
	public static float x = 120f;
    // Start is called before the first frame update
    void Start()
    {
        time = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        print("TIME SPENTT - FIX SCORING. GAME DESIGN SCORING RANK, HOW TO? FIVE STARS SHOULD HAVE A MIN CAP, OTHERWISE THE MAX A HUMAN CAN GET WOULD BE LIKE 4.8 STARS");
        remainder = 100f; //8999.0f - CountdownTimer.getCurrentTime(); //hmm
        remainder /= 2;
        if((remainder%60) > 9)		
			time.text = "TIME: 0" + Mathf.Floor(remainder/60) + ":" + Mathf.Floor(remainder%60);
		else
			time.text = "TIME: 0" + Mathf.Floor(remainder/60) + ":" + Mathf.Floor(remainder%60);
    }
}
