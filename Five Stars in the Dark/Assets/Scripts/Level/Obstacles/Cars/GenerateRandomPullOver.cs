using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomPullOver : MonoBehaviour
{
    [SerializeField]
    private AudioSource honk;
    // Start is called before the first frame update
    void Start()
    {
        AudioClip[] failureDialogues = Resources.LoadAll<AudioClip>("Audio/Police Chase");
        System.Random rand = new System.Random();
        int numDialogue = rand.Next(0, failureDialogues.Length);
        if (failureDialogues.Length > 0)
        {
            honk.clip = failureDialogues[numDialogue];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
