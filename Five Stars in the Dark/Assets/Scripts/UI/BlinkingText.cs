using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BlinkingText : MonoBehaviour
{
    private Text txt;
    private bool isTransparent = true;
    private float frequency = 1.5f;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime > frequency)
        {
            isTransparent = !isTransparent;
            startTime = Time.time;
        }

        if(isTransparent)
        {
            txt.CrossFadeAlpha(0f, 0.05f, false);
        }
        else
        {
            txt.CrossFadeAlpha(1f, 0.05f, false);
        }
    }

    void OnEnable()
    {
        txt = GetComponent<Text>();
        startTime = Time.time;
        txt.color = new Color(1f, 1f, 1f, 1f);
    }

    void OnDisable()
    {
        txt.color = new Color(1f, 1f, 1f, 0f);
    }
}
