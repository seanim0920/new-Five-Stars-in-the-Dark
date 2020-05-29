using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBumpSound : MonoBehaviour
{
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Played bump sound");
        if (col.gameObject.tag == "Player")
        {
            source.Play();
        }
    }
}
