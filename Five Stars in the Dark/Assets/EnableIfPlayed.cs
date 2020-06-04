﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableIfPlayed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlaythroughManager.hasPlayedLevel(PlaythroughManager.currentLevelIndex))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
