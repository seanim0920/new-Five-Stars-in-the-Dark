using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreListenerPause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().ignoreListenerVolume = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
