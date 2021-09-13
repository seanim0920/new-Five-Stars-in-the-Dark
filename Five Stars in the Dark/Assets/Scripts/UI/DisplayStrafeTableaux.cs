using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DisplayStrafeTableaux : MonoBehaviour
{
    private GameObject tableaux;
    private Transform tableauxType;
    private Text proceed;
    private float startTime = 0f;
    public int tableauxNum;
    private PauseMenu pauseScript;
    private GameObject pauseButtons;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        tableaux = GameObject.Find("TableauxMockups");
        tableauxType = tableaux.transform.GetChild(tableauxNum);
        tableauxType.gameObject.SetActive(true);
        proceed = tableaux.GetComponentInChildren<Text>();

        player = GameObject.Find("Player");
        pauseScript = GameObject.FindGameObjectWithTag("Main Camera").GetComponentInChildren<PauseMenu>();

        Debug.Log("Attempting to pause the game");
        pauseScript.pauseGame();
        pauseButtons = pauseScript.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        for (int i = 1; i < pauseButtons.transform.childCount; i++)
        {
            pauseButtons.transform.GetChild(i).gameObject.SetActive(false);
        }
        foreach (AudioSource audio in player.GetComponentsInChildren<AudioSource>(true))
        {
            audio.ignoreListenerPause = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if 1 seconds have elapsed && Accelerate to continue text is not enabled
        //if (!OverlayStatic.overlaid) im gay
        //{
            pauseScript.resumeGame();
            for (int i = 1; i < pauseButtons.transform.childCount; i++)
            {
                pauseButtons.transform.GetChild(i).gameObject.SetActive(true);
            }
            foreach (AudioSource audio in player.GetComponentsInChildren<AudioSource>(true))
            {
                audio.ignoreListenerPause = false;
            }
            tableauxType.gameObject.SetActive(false);
            Destroy(gameObject);
        //}
    }
}
