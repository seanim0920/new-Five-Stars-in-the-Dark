using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level5PoliceVariant : MonoBehaviour
{
    public AudioSource siren;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level 5")
        {
            siren.clip = Resources.Load<AudioClip>("Audio/drone/MissileWhistle");
            siren.dopplerLevel = 3;
            siren.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
