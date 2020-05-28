using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level5Variant : MonoBehaviour
{
    public AudioSource proximity;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level 5")
        {
            proximity.clip = Resources.Load<AudioClip>("Audio/drone/satellitebeep");
            proximity.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
