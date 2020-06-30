// NEED TO MAKE THIS FILE RESPOND TO EXPLICIT LEFT/RIGHT TURN INPUT
// AND NOT HARD-CODE KEYBOARD INPUT

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuickTurn : MonoBehaviour
{
    //public int direction;
    private AudioSource turnSound;
    private string turnDirection;
    private GameObject player;
    private float enterSpeed;
    private float enterSpeedometerAngle;
    private RotateSpeedometer speedometer;
    public PlayerControls playerCtrl;
    public MovementShake camMovement;
    public bool mustTurnLeft;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mustTurnLeft)
        {
            turnDirection = "Left";
        }
        else
        {
            turnDirection = "Right";
        }

        turnSound = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        GameObject canvas = GameObject.Find("Main Camera");
        speedometer = canvas.GetComponentInChildren<RotateSpeedometer>();
        enterSpeed = 0f;
        enterSpeedometerAngle = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Begin Quick Turn sequence
        if (other.transform.CompareTag("Player"))
        {
            playerCtrl = other.gameObject.GetComponent<PlayerControls>();
            other.GetComponentInParent<GamepadControl>().gamepad.Gameplay.Disable();
            Debug.Log("Turn " + turnDirection + "!"); // We potentially want to play a quick turn warning audio clip
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        float s = (mustTurnLeft ? -playerCtrl.movementSpeed : playerCtrl.movementSpeed) * 0.3f + playerCtrl.getStrafeAmount() * 3;
        playerCtrl.transform.position += new Vector3(s, 0, 0);
        //camMovement.sidewaysPosition += s;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //StartCoroutine(camMovement.resetSidewaysCoroutine());
            Destroy(gameObject);
        }
    }
}