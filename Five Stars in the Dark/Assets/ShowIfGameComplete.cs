using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIfGameComplete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlaythroughManager.hasPlayedLevel("Credits"))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
