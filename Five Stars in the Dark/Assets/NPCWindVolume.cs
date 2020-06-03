using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWindVolume : MonoBehaviour
{
    AudioSource wind;
    // Start is called before the first frame update
    void Start()
    {
        wind = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float speedDifference = Mathf.Abs(NPCMovement.getRelativeSpeed(transform.parent.gameObject));
        wind.volume = -1/(4*speedDifference+1)+1;
    }
}
