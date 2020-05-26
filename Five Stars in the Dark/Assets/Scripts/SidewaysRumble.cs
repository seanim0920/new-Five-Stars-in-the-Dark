using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysRumble : MonoBehaviour
{
    GameObject player;
    AudioSource rumble;
    // Start is called before the first frame update
    void Start()
    {
        rumble = GetComponent<AudioSource>();
        player = transform.parent.gameObject;
    }

    //private void Update()
    //{


    //    Vector3 posRelativeToPlayer = player.transform.InverseTransformPoint(transform.position);
    //    tires.volume = (1 + posRelativeToPlayer.y) / 2;
    //    filter.lowpassResonanceQ = ((1 + posRelativeToPlayer.y) / 2) * 5;
    //    filter.cutoffFrequency = ((1 + posRelativeToPlayer.y) / 2) * 500 + 200;
    //}

    //void OnCollisionStay2D(Collision2D col)
    //{
    //    if (col.gameObject.CompareTag("Guardrail"))
    //    {
    //        hitSoundObject.GetComponent<AudioSource>().volume = Math.abs((col.gameObject.transform.x - player.transform.x))/2;

    //        hitSoundObject.GetComponent<AudioSource>().pitch = 0.5f * controlFunctions.movementSpeed / controlFunctions.maxSpeed + 0.5f;
    //        TrackErrors.IncrementErrors(0.01f * controlFunctions.movementSpeed / controlFunctions.maxSpeed);
    //        controlFunctions.movementSpeed *= 0.995f;

    //        hitSoundObject.GetComponent<AudioSource>().volume = controlFunctions.movementSpeed / controlFunctions.maxSpeed;
    //        hitSoundObject.GetComponent<AudioSource>().pitch = 0.5f * controlFunctions.movementSpeed / controlFunctions.maxSpeed + 0.5f;
    //        TrackErrors.IncrementErrors(0.01f * controlFunctions.movementSpeed / controlFunctions.maxSpeed);
    //        controlFunctions.movementSpeed *= 0.995f;
    //    }
    //}
}
