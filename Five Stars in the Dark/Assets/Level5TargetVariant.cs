using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level5TargetVariant : MonoBehaviour
{
    public AudioSource signalScramble;
    public AudioSource ding;
    public AudioSource rev;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level 5")
        {
            signalScramble.clip = Resources.Load<AudioClip>("Audio/drone/LaserCharging");
            ding.clip = Resources.Load<AudioClip>("Audio/drone/LaserFiring");
            rev.clip = Resources.Load<AudioClip>("Audio/drone/Explosion");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
