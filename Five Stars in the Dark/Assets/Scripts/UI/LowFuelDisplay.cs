using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowFuelDisplay : MonoBehaviour
{
    Image image;
    AudioSource audio;
    private PlayerControls controls;
    private Color SmallColor;
    // Start is called before the first frame update
    void Start()
    {
        controls = GameObject.Find("Player").GetComponent<PlayerControls>();
        audio = GetComponent<AudioSource>();
        image = GetComponent<Image>();
        SmallColor = new Color(1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (CountdownTimer.getProgress() <= 0.1f)
        {
            if (!image.enabled)
            {
                audio.volume = 0.25f * (controls.currentSpeed / controls.maxSpeed) + 0.5f;
                GetComponent<AudioSource>().Play();
            }
            image.color = SmallColor;
            image.enabled = true;
        } else
        {
            image.enabled = false;
        }
    }
}
