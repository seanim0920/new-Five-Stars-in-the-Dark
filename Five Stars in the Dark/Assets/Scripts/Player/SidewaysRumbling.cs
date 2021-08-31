using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysRumbling : MonoBehaviour
{
    public GameObject leftRumbleSfx;
    public GameObject rightRumbleSfx;
    private float maxDistance = 17;
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
        bool backDetected = back.collider != null;
        bool frontDetected = front.collider != null;
        if ((backDetected && back.collider.gameObject.tag != "Zone") || (frontDetected && front.collider.gameObject.tag != "Zone"))
        {
            float backDistance = (back.point - (Vector2)transform.position - new Vector2(0,0.5f)).magnitude;
            float frontDistance = (front.point - (Vector2)transform.position + new Vector2(0,0.5f)).magnitude;
            float distance = Mathf.Min(backDistance, frontDistance);
            float ratio = (back.point - (Vector2)transform.position).magnitude / (front.point - (Vector2)transform.position).magnitude;
            //print("side ray distance" + distance);
            //leftSource.SetFloat("LowF", (minFrequency + (((maxFrequency - minFrequency) / (Mathf.Pow((hearingDistance - minDistance), sharpness))) * Mathf.Pow((distance - minDistance), sharpness))));

            float speedDifference = controls.currentSpeed;
            bool backCarExists = (backDetected &&
                (back.collider.gameObject.CompareTag("Car") || back.collider.gameObject.CompareTag("Target")));
            bool frontCarExists = (frontDetected &&
                (front.collider.gameObject.CompareTag("Car") || front.collider.gameObject.CompareTag("Target")));
            if (backCarExists || frontCarExists)
            {
                GameObject npc;
                if (backCarExists && frontCarExists)
                {
                    npc = backDistance < frontDistance ? back.collider.gameObject : front.collider.gameObject;
                } else
                {
                    npc = backCarExists ? back.collider.gameObject : front.collider.gameObject;
                }
                speedDifference = Mathf.Abs(NPCMovement.getRelativeSpeed(npc)) * 1.5f;
                //print("detected npc, speeddifference is " + speedDifference);
            }
            sound.pitch = 0.5f + (0.5f * speedDifference);
            sound.volume = 1.1f * (speedDifference-0.05f) / controls.maxSpeed;
            float pan = (left ? -1 : 1) * ((-1 / Mathf.Pow(maxDistance, 2)) * Mathf.Pow((distance-maxDistance), 2) + 1);
            //print("pan: " + pan);
            sound.panStereo = pan;
            filter.cutoffFrequency = Mathf.Exp(-ratio+7.3f);
        }
        else
        {
            sound.volume = 0;
        }
    }
}