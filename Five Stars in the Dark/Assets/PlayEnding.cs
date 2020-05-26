using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayEnding : MonoBehaviour
{
    public AudioSource levelDialogue;
    // Start is called before the first frame update
    void Start()
    {
        levelDialogue = GetComponent<AudioSource>();
        levelDialogue.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelDialogue.isPlaying)
        {
            LoadScene.Loader("EndScreen");
        }
    }
}
