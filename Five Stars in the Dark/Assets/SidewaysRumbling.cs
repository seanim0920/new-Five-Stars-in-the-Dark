using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysRumbling : MonoBehaviour
{
    public GameObject leftRumbleSfx;
    public GameObject rightRumbleSfx;
    private float maxDistance = 15;
    private PlayerControls controls;
    // Start is called before the first frame update
    void Start()
    {
        controls = GetComponent<PlayerControls>();
        leftRumbleSfx.GetComponent<AudioSource>().Play();
        rightRumbleSfx.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D leftEarBack = Physics2D.Raycast(transform.position - new Vector3(0,0.5f,0), -transform.right, maxDistance);
        RaycastHit2D leftEarFront = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), -transform.right, maxDistance);
        RaycastHit2D rightEarBack = Physics2D.Raycast(transform.position - new Vector3(0, 0.5f, 0), transform.right, maxDistance);
        RaycastHit2D rightEarFront = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.right, maxDistance);

        //distance alters panning
        //ratio between back and front rays alters low pass filter

        alterRumbleSound(leftEarBack, leftEarFront, leftRumbleSfx, true);
        alterRumbleSound(rightEarBack, rightEarFront, rightRumbleSfx, false);
    }

    void alterRumbleSound(RaycastHit2D back, RaycastHit2D front, GameObject soundObj, bool left)
    {
        AudioSource sound = soundObj.GetComponent<AudioSource>();
        AudioLowPassFilter filter = soundObj.GetComponent<AudioLowPassFilter>();
        if ((back.collider && back.collider.gameObject.tag != "Zone") || (front.collider && front.collider.gameObject.tag != "Zone"))
        {
            float distance = Mathf.Min((back.point - (Vector2)transform.position).magnitude, (front.point - (Vector2)transform.position).magnitude);
            float ratio = (back.point - (Vector2)transform.position).magnitude / (front.point - (Vector2)transform.position).magnitude;
            //print("side ray distance" + distance);
            //leftSource.SetFloat("LowF", (minFrequency + (((maxFrequency - minFrequency) / (Mathf.Pow((hearingDistance - minDistance), sharpness))) * Mathf.Pow((distance - minDistance), sharpness))));

            //sound.volume = 1 + (-1 / Mathf.Pow(2, 2)) * Mathf.Pow(distance, 2);

            sound.pitch = 0.5f + (0.5f * controls.movementSpeed / controls.maxSpeed);
            sound.volume = 1.1f * (controls.movementSpeed-0.05f) / controls.maxSpeed;
            float pan = (left ? -1 : 1) * (1 / Mathf.Pow(maxDistance, 2)) * Mathf.Pow(distance, 2);
            print("pan: " + pan);
            sound.panStereo = pan;
            filter.cutoffFrequency = Mathf.Exp(-ratio+7.3f);
        }
        else
        {
            sound.volume = 0;
        }
    }
}