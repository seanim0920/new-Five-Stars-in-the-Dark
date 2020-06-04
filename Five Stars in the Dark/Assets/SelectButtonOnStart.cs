using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButtonOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().mute = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
