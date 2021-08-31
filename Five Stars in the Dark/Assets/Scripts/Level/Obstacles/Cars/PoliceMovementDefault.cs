using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovementDefault : MonoBehaviour
{
    public AudioSource siren;
    public AudioSource drift;
    public AudioSource pullOver;
    [SerializeField] private float strafeSpeed;
    // private float acceleration = 0f;
    [SerializeField] private float eyesight;
    private NPCMovement moveFunctions;
    private bool seesPlayer = false;
    private bool coroutineRunning = false;
    private Vector3 direction;
    private GameObject obstacle;

    //enums to keep track of movement states, to prevent duplicate running of states
    private enum MoveState { None, Coasting, Ramming, Blocking, Warning };
    private MoveState currentMoveState = MoveState.Coasting;

    // Start is called before the first frame update
    void Start()
    {
        moveFunctions = GetComponent<NPCMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Determine which state to be in
        if(!seesPlayer)
        {
            direction = transform.up;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 120);
            if(hit.collider != null)
            {
                obstacle = hit.collider.gameObject;
            }
            // This section checks the front of the police car to find the player
            if (obstacle != null && obstacle.tag == "Player" && currentMoveState != MoveState.Warning)
            {
                print("sees player in front!");
                currentMoveState = MoveState.Warning;
                seesPlayer = true;
            }
        }
        if(!seesPlayer)
        {
            //this section checks both sides of the police car to find the player
            for (int i = -1; i <= 1; i += 2) // i only goes -1 then 1
            {
                direction = i * transform.right;
                
                obstacle = moveFunctions.SeesObstacle(direction);
                if (obstacle != null && obstacle.tag == "Player" && currentMoveState != MoveState.Blocking)
                {
                    print("sees player on the side!");
                    currentMoveState = MoveState.Blocking;
                    seesPlayer = true;
                    break;
                }
            }
        }
    }
    void FixedUpdate()
    {
        //to be more inline with the narrative, maybe the police is just active for now until the player screws up.

        if (currentMoveState == MoveState.Coasting)
        {
            StartCoroutine(moveFunctions.Coast());
            currentMoveState = MoveState.None;
        }
        else if(currentMoveState == MoveState.Blocking)
        {
            if(obstacle != null) // Just for safety
            {
                if(!coroutineRunning)
                {
                    StartCoroutine(blockPlayer(obstacle, direction));
                }
            }
        }
        else if(currentMoveState == MoveState.Warning)
        {
            // Pull Over Warning, which is made up of slow down to player's speed
            // and honk (Pull over sound)
            if(obstacle != null) // Just for safety
            {
                if(!coroutineRunning)
                {
                    StartCoroutine(warnPlayer(obstacle));
                }
            }
        }
    }
    IEnumerator blockPlayer(GameObject player, Vector3 direction)
    {
        coroutineRunning = true;
        print("trying to ram player");
        Vector3 posRelativeToPlayer = player.transform.InverseTransformPoint(transform.position);
        while (posRelativeToPlayer.y <= 1) //we will assume 50 is half the car's length
        {
            posRelativeToPlayer = player.transform.InverseTransformPoint(transform.position);
            print("catching up to player" + posRelativeToPlayer);
            yield return new WaitForFixedUpdate();
        }
        while (posRelativeToPlayer.y > 0)
        {
            posRelativeToPlayer = player.transform.InverseTransformPoint(transform.position);
            print("aligning with player");
            //if this car is getting ahead of the player, slow down until it's aligned with the player, then ram into them.
            moveFunctions.currentSpeed *= 0.98f;
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(moveFunctions.SwitchLaneRight(direction == transform.right, 0.5f));
        drift.Play();
        currentMoveState = MoveState.Coasting;
    }

    IEnumerator warnPlayer(GameObject player)
    {
        coroutineRunning = true;
        float startTime = Time.time;
        Vector3 posRelativeToPlayer = player.transform.InverseTransformPoint(transform.position);

        // Stalk the player for a second
        float originalSpeed = moveFunctions.currentSpeed;
        if(posRelativeToPlayer.x < 5 && 
           posRelativeToPlayer.x > -5)
        {
            moveFunctions.currentSpeed = player.GetComponent<PlayerControls>().currentSpeed;
            yield return new WaitForSeconds(1f);
        }
        else
        {
            Debug.Log("Saw the player, but not really");
        }

        // If they ain't givin' way, tell em to pull over, duh
        if(posRelativeToPlayer.x < 5 && 
              posRelativeToPlayer.x > -5)
        {
            pullOver.Play();
        }
        else
        {
            Debug.Log("Player moved out of the way");
        }

        if(pullOver.isPlaying)
        {
            while(pullOver.isPlaying)
            {
                moveFunctions.currentSpeed = player.GetComponent<PlayerControls>().currentSpeed;
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(1f);
        }

        // However the player breaks out of that loop, be on your merry way
        moveFunctions.currentSpeed = originalSpeed;
        currentMoveState = MoveState.Coasting;
        yield break;
    }
}
