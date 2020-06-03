﻿using UnityEngine;

public class ToggleBlackScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject blackScreen;
    private bool isBlack = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash) && Input.GetKeyDown(KeyCode.Comma) && Input.GetKeyDown(KeyCode.X))
        {   
            isBlack = !isBlack;
        }
    }
    private void FixedUpdate() {
        if (isBlack == true)
        {
            blackScreen.SetActive(true);
        } 
        else
        {
            blackScreen.SetActive(false);
        }
    }
}
