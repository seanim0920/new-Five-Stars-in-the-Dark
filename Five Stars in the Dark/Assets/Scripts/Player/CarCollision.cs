using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarCollision : MonoBehaviour
{
    public AudioSource lightCollisionSound;

    private Rigidbody2D body;
    private PlayerControls controlFunctions;
    private SteeringWheelInput wheelFunctions;

    private GameObject hitSoundObject;
    public GameObject situationalDialogues;
    //collision sound
    string[] obstacleTags = { "Car", "Curb", "Guardrail", "Pedestrian", "Stop", "Target" };

    // Start is called before the first frame update
    void Start()
    {
        controlFunctions = GetComponent<PlayerControls>();
        wheelFunctions = GetComponent<SteeringWheelInput>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator disableNPCMomentarily(GameObject NPC, float speed)
    {
        NPCMovement movement = NPC.GetComponent<NPCMovement>();
        Rigidbody2D NPCbody = NPC.GetComponent<Rigidbody2D>();
        movement.movementSpeed = 0;
        movement.enabled = false;
        NPCbody.AddForce((NPC.transform.position - transform.position).normalized * speed * 70, ForceMode2D.Impulse);
        if (speed > 0.5f)
        {
            if (speed > 0.8f)
            {
                NPC.transform.Find("SkidSfx").GetComponent<AudioSource>().Play();
            }
            movement.neutralSpeed = 0;
        }
        while (NPCbody != null && NPCbody.velocity.magnitude > 0.1f)
        {
            yield return new WaitForFixedUpdate();
        }
        if (NPCbody == null) yield break;
        NPCbody.velocity *= 0;
        if (speed > 0.5f)
        {
            NPC.transform.Find("AlarmSfx").GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1);
            if (speed > 0.8f)
            {
                NPC.transform.Find("SmokeSfx").GetComponent<AudioSource>().Play();
                yield return new WaitForSeconds(1);
            }
            if (NPC != null)
            {
                NPC.transform.Find("OpenDoorSfx").GetComponent<AudioSource>().Play();
                NPC.tag = "Stopped";
            }
        }
        else
        {
            NPC.transform.Find("HonkSfx").GetComponent<AudioSource>().Play();
            if (movement.neutralSpeed > 0)
                movement.enabled = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (System.Array.IndexOf(obstacleTags, col.gameObject.tag) != -1)
        {
            //factor speed in, faster speed means bigger error
        }

        hitSoundObject = col.gameObject;
        Debug.Log(hitSoundObject);
        hitSoundObject.GetComponent<AudioSource>().Play();

        if (col.gameObject.CompareTag("Car") || col.gameObject.CompareTag("Target") || col.gameObject.CompareTag("Stopped"))
        {
            wheelFunctions.PlayFrontCollisionForce();

            NPCMovement movementScript = col.gameObject.GetComponent<NPCMovement>();
            float speedDifference = Mathf.Abs(movementScript.movementSpeed - controlFunctions.movementSpeed);
            TrackErrors.IncrementErrors(speedDifference);

            body.bodyType = RigidbodyType2D.Dynamic;
            body.AddForce((transform.position - col.gameObject.transform.position).normalized * speedDifference * 50, ForceMode2D.Impulse);
            StartCoroutine(controlFunctions.impactCoroutine(body.velocity));
            if (!col.gameObject.CompareTag("Target"))
                StartCoroutine(disableNPCMomentarily(col.gameObject, speedDifference));

            if (hitSoundObject.GetComponent<ObstacleFailure>())
                hitSoundObject.GetComponent<ObstacleFailure>().playFailure(Camera.main.transform.position);
        }
        if (col.gameObject.CompareTag("Guardrail"))
        {
            if (col.gameObject.transform.position.x > transform.position.x)
            {
                print("blocked right");
                controlFunctions.blockDirection(1);
            }
            else
            {
                print("blocked left");
                controlFunctions.blockDirection(-1);
            }
            if (!controlFunctions.enabled)
            {
                lightCollisionSound.Play();
            }
        }

        //these pull a random hurtsound to play
        int x = 4; // Random.Range(-2, 1) + (GetNumericValue(SceneManagment.Scene.name[6]) * 3);
        AudioClip passengerHurt = Resources.Load<AudioClip>("Audio/dialogue/hurt" + x);

        print("hitting a zone?" + (!col.gameObject.CompareTag("Zone")));
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Guardrail"))
        {
            hitSoundObject.GetComponent<AudioSource>().volume = controlFunctions.movementSpeed / controlFunctions.maxSpeed;
            hitSoundObject.GetComponent<AudioSource>().pitch = 0.5f * controlFunctions.movementSpeed / controlFunctions.maxSpeed + 0.5f;
            TrackErrors.IncrementErrors(0.01f * controlFunctions.movementSpeed / controlFunctions.maxSpeed);
            controlFunctions.movementSpeed *= 0.995f;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Guardrail"))
        {
            controlFunctions.blockDirection(0);
            col.gameObject.GetComponent<AudioSource>().Stop();
        }

    }
}